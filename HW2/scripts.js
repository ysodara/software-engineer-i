$(document).ready(function(){
        $(".btn-add").click(function(){
            var condition = $("#condition").val();
            var make = $("#make").val();
            var model = $("#model").val();
            var year = $("#year").val();
            var markup =  "<tr><td>"+ condition + "</td><td>" +make +"</td><td>"+ model+"</td><td>"+ year+"</td></tr>";
            $("table tbody").append(markup);
        
            });
    });













