﻿$(document).ready(function() {

$('div.demo-show:eq(0)> h4,#DetailedInfo:eq(0)> h4,#Download:eq(0)> h4').click(function () {
$(this).next().slideToggle('slow');
$(this).find('a').toggleClass('expanded').toggleClass('collapsed');
  });
  
  $('#Div1Title,#DivPartnerPr,#Div2Title').click(function () {
$(this).next().slideToggle('slow');

  });
  
  
  
  
  
 $('#NoData').show();
 $('#ProjectMenu').show();
 
$('#Download:eq(0) > h4 a.collapsed,#DetailedInfo:eq(0) > h4 a.collapsed').parent().css('display', 'none');
 
try{
        var mydiv = document.createElement("div");
        mydiv.className = "toggleLoginButton";
       // mydiv.innerHTML = '<a class="toggleLoginButtonLink" href="#">Partner Login</a>';
        $("#LoginDiv").prepend(mydiv);
        $("#LoginDiv").addClass("loginDropdown");
        $("#LoginDiv").css("overflow", "visible");

        $('a.toggleLoginButtonLink').click(
            function(){
                $('#LoginDiv .block-inner').toggle(500);
                $('#LoginDiv a.toggleLoginButtonLink').toggleClass("active",500);
               
            }
        );
    } catch(e) {}



// We need to do some browser sniffing to weed out IE 6 only
// because only IE6 needs this hover hack.
if (document.all && !window.opera && (navigator.appVersion.search("MSIE 6.0") != -1) && $.browser.msie) {
    function IEHoverPseudo() {
        $("ul.nice-menu li.menuparent").hover(function() {
            $(this).addClass("over").find("> ul").show().addShim();
        }, function() {
            $(this).removeClass("over").find("> ul").removeShim().hide();
        });
        // Add a hover class to all li for CSS styling. Silly naming is done
        // so we don't break CSS compatibility for .over class already in use
        // and due to the fact that IE6 doesn't understand multiple selectors.
        $("ul.nice-menu li").hover(function() {
            $(this).addClass("ie-over");
        }, function() {
            $(this).removeClass("ie-over");
        });
    }

    // This is the jquery method of adding a function
    // to the BODY onload event.  (See jquery.com)
    $(document).ready(function() {
        IEHoverPseudo()
    });
}

$.fn.addShim = function() {
    return this.each(function() {
        if (document.all && $("select").size() > 0) {
            var ifShim = document.createElement('iframe');
            ifShim.src = "javascript:false";
            ifShim.style.width = $(this).width() + 1 + "px";
            ifShim.style.height = $(this).find("> li").size() * 23 + 20 + "px";
            ifShim.style.filter = "progid:DXImageTransform.Microsoft.Alpha(style=0,opacity=0)";
            ifShim.style.zIndex = "0";
            $(this).prepend(ifShim);
            $(this).css("zIndex", "99");
        }
    });
};

$.fn.removeShim = function() {
    return this.each(function() {
        if (document.all) $("iframe", this).remove();
    });
};



  });



function resetTimer(timer)
{

clearTimeout(timer);
setTimeout(redirectfunction,6000);

}

function Help_Click()
{
//ChildWindow = window.open('Calendar.aspx?FormName=' + document.forms[0].name + '&CtrlName=' + CtrlName, "PopUpCalendar", "width=270,height=300,top=200,left=200,toolbars=no,scrollbars=no,status=no,resizable=no");
temp = window.open('help.htm','mywindow','width=600,height=400,scrollbars=yes');

}


function EDF_ShortDescription()
{
 temp = window.open('http://www.series.upatras.gr/sites/default/files/pri/drafts/Deliverable2_1_0.pdf','mywindow','width=600,height=800,scrollbars=yes');

}
function ReturnHome()
{
window.location= "http://www.series.upatras.gr";

}
function Terms_of_use()
{
 temp = window.open('Terms_Of_Use.aspx','mywindow','width=600,height=400,scrollbars=yes');

}




$(document).ready(function() {
	//On Click Event
	$("ul.tabs li").click(function() {

		$("ul.tabs li").removeClass("active"); //Remove any "active" class
		$(this).addClass("active"); //Add "active" class to selected tab
		$(".tab_content").hide(); //Hide all tab content

		var activeTab = $(this).find("a").attr("href"); //Find the href attribute value to identify the active tab + content
		$(activeTab).fadeIn(); //Fade in the active ID content
		
		document.getElementById("x").value=activeTab;
	
		return false;
	
	});



$("#GoogleDivs div label").mouseover(function(){
$(this).css('text-decoration','underline');
$(this).css('cursor','pointer');
});

$("#GoogleDivs div label").mouseout(function(){
$(this).css('text-decoration','none');
$(this).css('cursor','default');
});



});

function activateTab(State,ClickedTree)
{

  
        $("ul.tabs li").removeClass("active"); //Remove any "active" class
        $(".tab_content").hide(); //Hide all content//0 based, so 1 = 2nd
        var activeTab;
        
        switch (State)
        {
        case "Initial":
            activeTab = 0;
            break;
        case "DetailedInfo":
            activeTab = 1;
            break;
        case "Download":
            activeTab = 2;
            break;
        case "Search":
            activeTab = 4;
            break;
        case "Home":
            activeTab = 5;
            break;
        case "LabInfo":
            activeTab = 6;
            break;
        case "Asearch":
            activeTab = 7;
            break;
            
}



        if (activeTab < 4) {
            $("#rightTopPane").css('display', 'inline');
            $("#rightSplitterContainer").css('visibility', 'visible');
            
            $("#SearchDiv").css('display', 'none');
            $("#FreeSearch").css('display', 'none');
            $("#HomeDiv").css('display', 'none');
            $("#LabInfoDiv").css('display', 'none');
            $("#Search_Criteria").css('display', 'none');
            $("#GoogleDivs").css('display', 'none');
            $("ul.tabs li").eq(activeTab).addClass("active").show();
            $(".tab_content").eq(activeTab).fadeIn();
  

        }
        else if (activeTab == 4) 
        {

            
            $("#rightTopPane").css('display', 'none'); //Remove any "active" class
		$("#HomeDiv").css('display', 'none');
		$("#SearchDiv").css('display', 'none');
		$("#GoogleDivs").css('display', 'inline');
		$("#FreeSearch").css('display', 'inline');
		$("#rightSplitterContainer").css('visibility','visible');
		 $("#LabInfoDiv").css('display','none');
        }
         else if (activeTab == 5) {
            $("#HomeDiv").css('display', 'inline');
            $("#rightSplitterContainer").css('visibility', 'visible');
            
            $("#rightTopPane").css('display', 'none');
            $("#SearchDiv").css('display', 'none');
            $("#LabInfoDiv").css('display', 'none');
            $("#rightTopPane").css('display', 'none');
            $("#Search_Criteria").css('display', 'none');
            $("#GoogleDivs").css('display', 'none');
            $("#FreeSearch").css('display', 'none');
          
        }
        else if (activeTab == 6) {
            $("#LabInfoDiv").css('display', 'inline');
            $("#rightSplitterContainer").css('visibility', 'visible');
            
            $("#rightTopPane").css('display', 'none');
            $("#SearchDiv").css('display', 'none');
            $("#FreeSearch").css('display', 'none');
            $("#Search_Criteria").css('display', 'none');
            $("#GoogleDivs").css('display', 'none');
            $("#HomeDiv").css('display', 'none');
            $("#rightTopPane").css('display', 'none');
            
        }
        else if (activeTab == 7) {

            $("#rightTopPane").css('display', 'none'); //Remove any "active" class
            $("#HomeDiv").css('display', 'none');
            
            $("#SearchDiv").css('display', 'inline');
            $("#GoogleDivs").css('display', 'inline');
            $("#FreeSearch").css('display', 'none');
            $("#rightSplitterContainer").css('visibility', 'visible');
            $("#LabInfoDiv").css('display', 'none');
        }


		////LabInfoDiv\
       // alert(ClickedTree);
       if(ClickedTree == "P")
       {
     
     
        $("#Div2Title").css('display','none');
        $("#Div2").css('display','none');	
        $("#DivPartnerPr").css('display','none');
         $("#Div2Title").css('display','none');
        $("#Div1").css('height','89%');
	    
	    }
       else if(ClickedTree == "PS")
       {
          
	       
            $("#Div2Title").css('display','block');
	        $("#Div2").css('display','block');
	        $("#Div1").css('height','49%');
	        $("#Div2").css('height','40%');
	        
	         
 


	    }
	    else if (ClickedTree == "PP") {

	        $("#DivPartnerPr").css('display', 'block');
	        $("#DivPartnerTree").css('display', 'block');
	        $("#Div1").css('height', '49%');
	        $("#DivPartnerTree").css('height', '40%');
	        
	       


	    }
	    else if (ClickedTree == "PSP") {


	        $("#DivPartnerPr").css('display', 'block');
	        $("#Div2Title").css('display', 'block');
	        $("#DivPartnerTree").css('display', 'block');
	        $("#Div2").css('display', 'block');
	        
	        $("#Div1").css('height', '30%');
	        $("#Div2").css('height', '29%');
	        $("#DivPartnerTree").css('height', '30%');
	        

	    }

};


function SearchDiv()
{

var activeTab = 3;
$("ul.tabs li").removeClass("active"); //Remove any "active" class
$(".tab_content").hide(); //Hide all content//0 based, so 1 = 2nd
$("ul.tabs li").eq(activeTab).addClass("active").show();
$(".tab_content").eq(activeTab).fadeIn();
};


function activateSearch()
{

  document.getElementById('PrTitleLabel').innerText="Search Funtionality";

    
		$("#rightTopPane").css('display', 'none'); //Remove any "active" class
		$("#HomeDiv").css('display', 'none');
		$("#SearchDiv").css('display', 'inline');
		$("#GoogleDivs").css('display', 'inline');
		$("#FreeSearch").css('display', 'none');
		$("#rightSplitterContainer").css('visibility','visible');
		 $("#LabInfoDiv").css('display','none');
};




function SearchView()
{
      activateSearch();
};


function FindActiveTab()
{
    var activeTab ;
    if($("#GeneralInfo").is(":visible"))
    activeTab="GeneralInfo";
    else if ($("#Download").is(":visible"))
    activeTab="Download";
    else if ($("#DetailedInfo").is(":visible"))
    activeTab="DetailedInfo";
    return activeTab ; 
}
