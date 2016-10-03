var questionsBaseAddress = "/api/questions/";

var viewModel = {
    questions: ko.observableArray(),
    addQuestion: function (question) {
        this.questions.push(question);
        // timeout to make css animation work
        setTimeout(function () { $("table#questions tbody tr#q" + question.id).addClass("transparent") }, 100);
    },
    updateQuestion: function (id, question) {
        var updateThisQuestion = ko.utils.arrayFirst(this.questions(), function (item) {
            return parseInt(item.id) === parseInt(id);
        });
        this.questions.remove(updateThisQuestion);
        this.questions.push(question);
        $("table#questions tbody tr#q" + id).addClass("transparent");
    }
};

var myArraySubscription = viewModel.questions.subscribe(onMyArrayChange);

$.connection.hub.logging = true;
var questionsHub = $.connection.questionHub;

$.connection.hub.start().done(function () {

    $.getJSON(questionsBaseAddress, function (data) {
        viewModel.questions(data);
        $("table#questions tbody tr").removeClass("success").removeClass("danger");
    });

    $("#status").text("Live").removeClass("label-info").addClass("label-success");

});

questionsHub.client.newQuestion = function (question) {
    // TODO : Freeze update => sore in localstorage
    viewModel.addQuestion(question);
};

ko.applyBindings(viewModel);

$(document).ready(function() {
    $("#questions").on("click", ".rerun", function () {
        var $this = $(this);
        var updatingText = "Updating...";
        if ($this.text() !== updatingText) {
            var originalText = $this.clone().text();
            $this.text(updatingText);
            var questionId = $this.closest("tr").attr("id").replace("q", "");
            
            $.getJSON(questionsBaseAddress + "rerun/" + questionId, function (data) {
                viewModel.updateQuestion(questionId, data);
                $this.text(originalText);
            });
        }
    });
});

function onMyArrayChange() {
    //Remove the subscription before sorting, to prevent an infinite loop
    myArraySubscription.dispose();
    myArraySubscription = null;
    //Force a sort of the array here. 
    viewModel.questions.sort(function (l, r) { return l.added < r.added ? 1 : -1 });
    //Re-subscribe
    myArraySubscription = viewModel.questions.subscribe(onMyArrayChange);
}

ko.extenders.date = function (target, format) {
    return ko.computed({
        read: function () {
            var value = target();
            if (typeof value === "string") {
                value = new Date(value);
            }
            format = typeof format === "string" ? format : undefined;
            value = Globalize.format(value, format);
            return value;
        },
        write: target
    });
}