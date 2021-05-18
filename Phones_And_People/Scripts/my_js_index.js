
    $(document).ready(function () {
            //alert("hi");
        var searchPopup = document.querySelectorAll(".btn-search");
        var searchSubmit = document.getElementById("srch-submit");
        var buttons = document.querySelectorAll('i');


        //searchSubmit.textContent
        //searchPopup.hide(100);
        for (let i = 0; i < buttons.length; i++)
        {
            place_arrows(buttons[i]);
        }
        for (let i = 0; i < buttons.length; i++)
        {
            buttons[i].addEventListener("click", function () { change_Filter_Image(buttons[i]) });
        }
        for (let i = 0; i < searchPopup.length; i++)
        {
            searchPopup[i].addEventListener("click", function () { show_search_fields(searchPopup[i]) });
        }
            //searchPopup.addEventListener("click", show_search_fields);
            //searchSubmit.addEventListener("click", search_people);
        });
        function show_search_fields(butt) {
            var popup = butt.nextSibling.nextSibling;

            if (typeof popup !== 'undefined') {
                if (popup.classList.contains('show')) {
        popup.classList.remove("show");
                    //document.querySelector("[name ='title']");
                }
                else {
        popup.classList.toggle('show');
                }
            }
        }

        function change_Filter_Image(sort_btn) {

            //alert(sort_btn.nextSibling.textContent);
            var sortOrder = sort_btn.nextSibling.textContent.includes('Title')? "@ViewBag.TitleSort" :
                    sort_btn.nextSibling.textContent.includes('Last') ? "@ViewBag.LnameSort" :
                sort_btn.nextSibling.textContent.includes('Name') ? "@ViewBag.FnameSort" :
                        "@ViewBag.Date";

            var dateFiltY = "@ViewBag.CurrYearFilt" === "" ? null : Number("@ViewBag.CurrYearFilt");
            var dateFiltM = "@ViewBag.CurrMonthfilt" === "" ? null : Number("@ViewBag.CurrMonthfilt");
            var dateFiltD = "@ViewBag.CurrDayfilt" === "" ? null : Number("@ViewBag.CurrDayfilt");
            var currFilter = "@ViewBag.CurrentFilter" === "" ? null : ("@ViewBag.CurrentFilter").toString();

            var MyAppUrlSettings = {
        MyUsefulUrl: '@Url.Action("Index","Home")'
            }

            $.ajax({
                url: MyAppUrlSettings.MyUsefulUrl,
                type: "POST",
                data: {
                    "CurrentFilter": currFilter,
                    "sortOrder": sortOrder,
                    "y": dateFiltY,
                    "m": dateFiltM,
                    "d": dateFiltD
                },
                success: function (data) {
                    $("#all").html(data);
                }
            });
        }
        function search_people() {
            var searchForm= document.getElementsByName("search");
            var searchElem = document.getElementsByName("title");

            searchForm.action = "SearchByName";

        }

        function place_arrows(sort_btn) {
            var field = sort_btn.nextSibling.textContent;
            var titleFilt = "@ViewBag.CurrentSort";

            if (field.includes("Title") && "@ViewBag.CurrentSort".startsWith("T")) {
                if ("@ViewBag.CurrentSort".includes("Asc")) {
        sort_btn.setAttribute("class", "fas fa-angle-down btn btn-default");
                }
                else
                    sort_btn.setAttribute("class", "fas fa-angle-up btn btn-default");
            }
            else if (field.includes("Last") && "@ViewBag.CurrentSort".startsWith("L")) {
                if ("@ViewBag.CurrentSort".includes("Asc"))
                    sort_btn.setAttribute("class", "fas fa-angle-down btn btn-default");
                else
                    sort_btn.setAttribute("class", "fas fa-angle-up btn btn-default");
            }
            else if (field.includes("Name") && "@ViewBag.CurrentSort".startsWith("F")) {
                if ("@ViewBag.CurrentSort".includes("Asc"))
                    sort_btn.setAttribute("class", "fas fa-angle-down btn btn-default");
                else
                    sort_btn.setAttribute("class", "fas fa-angle-up btn btn-default");
            }
            else if ("@ViewBag.CurrentSort".startsWith("D")) {
                if ("@ViewBag.CurrentSort".includes("Asc"))
                    sort_btn.setAttribute("class", "fas fa-angle-up btn btn-default");
                else
                    sort_btn.setAttribute("class", "fas fa-angle-down btn btn-default");
            }
            else {
            }
        }