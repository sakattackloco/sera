<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SERIESnew_db._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Import Namespace="SERIESnew_db"%>
<%@ Import Namespace="MySql.Data"%>
<%@ Import Namespace="MySql.Data.MySqlClient"%>
<%@ Import Namespace="System.Configuration"%>
<%@ Import Namespace="System.Security.Cryptography"%>
<%@ Import Namespace="System.Security.Cryptography.X509Certificates"%>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <META HTTP-EQUIV="Pragma" CONTENT="no-cache"> <META HTTP-EQUIV="Expires" CONTENT="-1">
<title>SERIES Data Access Portal</title>
<script type="text/javascript" src="jquery/jquery-1.4.3.min.js"></script>

<script src="jquery/my_jquery_functions.js" type="text/javascript"></script>
<script type="text/javascript" src="jquery/jquery.cycle.all.latest.js"></script>
<link href="Style.css" rel="stylesheet" media="screen" type="text/css" />

</script>

	<script type="text/javascript">
		$(document).ready(function() {
			
  $('.pics').cycle({
		fx: 'fade' // choose your transition type, ex: fade, scrollUp, shuffle, etc...
	});
	
	
	


		});
			
			</script>

</head>



<% 
   string ActiveTab = Session["ActiveView"].ToString();
   string LeftState = Session["LeftState"].ToString();
        Response.Write("<body onload=activateTab('" + ActiveTab +"','"+LeftState+ "')>");         
    %>    
<body>
    <form id="form1"  runat="server" style="width:100%; height:100%;overflow:visible;">
   <div class="wrapper">
<div id="header">

<input runat="server" type="hidden" name="x" id="x" value="" />
<div id="header-inner">
<div id="LogoTitle"><img src="images/seriestplv3_logo.png" style="cursor:pointer;"  onclick="ReturnHome()"/></div>
<div id="SiteImg" ></div>
 
<div id="LoginDiv" runat="server">

 <div id="div_wrongPass" runat="server">
	<label>Partner not found</label>  
 </div> 
  <div class="toggleLoginButton">
	 <a href="#" class="toggleLoginButtonLink">Partner Login</a>
 </div>
 <div class="block-inner">

 <div>
 	<img  src="images/login_user_icon_trans.png" />
    	<asp:TextBox ID="TxtBxUsername" CssClass="textBox" runat="server"></asp:TextBox> 
    	<img  src="images/login_pass_icon_trans.png"/>
    	<asp:TextBox ID="TxtBxPassword"  TextMode="Password" CssClass="textBox" runat="server"></asp:TextBox>
    	
    	<asp:Button ID="BtnLogin" runat="server" Text="Partner login"  OnClick="BtnClick_Login"/>
 </div>
 </div>
</div>
<div id ="LoggedDiv" runat="server">
<asp:Label ID="lbl_WelcomeMsg" runat="server"></asp:Label>
<asp:LinkButton ID="lnlbtn_logout" runat="server" OnClick="ImgLogout_Click"  ForeColor="#dccf46" >Logout</asp:LinkButton>
</div>
</div>
   

</div>


   <!-- MENU -->

    <!--containers-->
   
        
<div   width = "100%" height= "40px">
<div id="navbar"><div id="navbar-inner">
<%
    Response.Write(DownloadString());
%>

</div></div>

        <!-- /#navbar-inner, /#navbar -->
    <!-- endof containers -->

<!-- ENDOF MENU -->





<div id="splitterContainer">



	<div id="leftPane">
	<div class="">
    <div   class="orderby"> Projects order by: <asp:DropDownList  id="OrderByList"
                    AutoPostBack="True"
                    OnSelectedIndexChanged="DropDownChgd"
                    runat="server">

                  <asp:ListItem Selected="True" Value="StartDate">[Creation Date]</asp:ListItem>
                  <asp:ListItem Value="Title">[Project Name]</asp:ListItem>
                  <asp:ListItem Value="Lab">[Laboratory Name]</asp:ListItem>
                  
               </asp:DropDownList></div>
     <div   class="orderby"> Internal Representation: <asp:DropDownList  id="PrInternalList"
                    AutoPostBack="True"
                    OnSelectedIndexChanged="DropDownChgd"
                    runat="server">

                  <asp:ListItem Selected="True" Value="Levels">[Flat]</asp:ListItem>
                  <asp:ListItem Value="Simple">[Layers]</asp:ListItem>
                                </asp:DropDownList></div>
         </div>      
	<div id="Div1Title" class="TreeTitleDiv" runat="server"><span class="TreeTitleText">Public Projects</span> </div>
		
		<div id="div1wrapper" runat="server">
	    <div id="Div1" runat="server">
		  <asp:TreeView ID="TreeView2" runat="server"  OnSelectedNodeChanged = "loadData"  
            OnTreeNodePopulate = "PopulateNodes" ImageSet="Custom" PathSeparator="/" 
                ForeColor="#52463F" Font-Names="'Helvetica Neue',Helvetica,Arial,sans-serif, Tahoma, Calibri, Verdana,'Bitstream Vera Sans' ;" Font-Size=" 0.85em"
             Font-Bold="false" ShowLines="true">
            <Nodes>
                <asp:TreeNode Text = "Public Projects" Value = "RootNode" PopulateOnDemand ="true"/>
            </Nodes>
            <ParentNodeStyle Font-Bold="False" />
            <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
            <SelectedNodeStyle Font-Underline="True" ForeColor="Brown" BackColor="LightBlue"
                HorizontalPadding="0px" VerticalPadding="0px" />
            <NodeStyle  
                HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
        </asp:TreeView>
		</div>
        </div>
        <div id="DivPartnerPr" runat="server"><span class="TreeTitleText">Partner Projects</span> </div>
		<div id="divPartnerPrwrapper" runat="server">
		<div id="DivPartnerTree" runat="server">
		    <asp:TreeView ID="PartnerTree" runat="server"  OnSelectedNodeChanged = "loadData"  Visible="false"   OnTreeNodePopulate = "PopulateNodes"    
             ImageSet="Custom" PathSeparator="/"  ForeColor="#52463F" Font-Names="'Helvetica Neue',Helvetica,Arial,sans-serif, Tahoma, Calibri, Verdana,'Bitstream Vera Sans' ;" Font-Size=" 0.85em"
             Font-Bold="false" ShowLines="true">
             <Nodes>
                <asp:TreeNode Text = "Partner Project" Value = "Partner root" PopulateOnDemand ="true" Expanded="true" />
            </Nodes>
            <ParentNodeStyle Font-Bold="False" />
            <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
            <SelectedNodeStyle Font-Underline="True" ForeColor="Brown"  BackColor="LightBlue" 
                HorizontalPadding="0px" VerticalPadding="0px" />
            <NodeStyle
                HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
        </asp:TreeView>
        </div>
        </div>

		<div id="Div2Title" runat="server"><span class="TreeTitleText">Search Results</span> </div>
		<div id="div2wrapper" runat="server">
		<div id="Div2">
		    <asp:TreeView ID="SearchTree" runat="server"  OnSelectedNodeChanged = "loadData"  Visible="false"      
             ImageSet="Custom" PathSeparator="/"  ForeColor="#52463F" Font-Names="'Helvetica Neue',Helvetica,Arial,sans-serif, Tahoma, Calibri, Verdana,'Bitstream Vera Sans' ;" Font-Size=" 0.85em"
             Font-Bold="false" ShowLines="true">
             <Nodes>
                <asp:TreeNode Text = "Search Results" Value = "Search Results" PopulateOnDemand ="true" Expanded="true" />
            </Nodes>
            <ParentNodeStyle Font-Bold="False" />
            <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
            <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" 
                HorizontalPadding="0px" VerticalPadding="0px" />
            <NodeStyle Font-Size="10pt"
                HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
        </asp:TreeView>
        </div>
   </div>
	</div>
	<!-- #leftPane -->


	
	
	<div id="rightPane">
	
	    <div id="toolbar">
	<div id="ProjectTitleDiv"><asp:Label  runat="server" id="PrTitleLabel"></asp:Label><asp:Label  runat="server" id="PrIDLabel" visible = "false"></asp:Label></div>

	<div onclick="Help_Click()"  style="float:right; width:35px; height:33px; cursor:pointer; background:url(newcss/search_info.jpg) no-repeat; "><p></p></div>      
	<div onclick="activateSearch()" style="float:right; width:118px; height:33px; cursor:pointer; "><img  alt="SearchHelp" src="newcss/search.jpg" border="0"/>   
	  </div>
	</div>
		<div id="rightSplitterContainer">
		 <div id="rightTopPane">
         
         <div id="NavBar2">
	                      <ul class="tabs">
        <li><a href="#GeneralInfo">Project Info</a></li>
        <li><a href="#DetailedInfo">Detailed Information</a></li>
        <li><a href="#Download">Downloadable Items</a></li>
      
    </ul>
    
</div>

    <div class="tab_container">
        <div id="GeneralInfo" class="tab_content">
            <!--Content-->
     
    <br />



           
    <asp:GridView ID="GrdGeneralPrInfo" runat="server" DataSourceID="InitialPrInfo" AutoGenerateColumns="False"
          CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"
            CellPadding="4" ForeColor="#52463F" HorizontalAlign="Center" Visible="False" 
                            Font-Size="0.85em" >
            <RowStyle BackColor="#EFF3FB" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1"  ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
         		 <Columns>
                        <asp:BoundField  DataField="title" HeaderText="Project Title"/>
                        <asp:BoundField  DataField="Startdate" HeaderText="Project Startdate"/>
                        <asp:BoundField  DataField="enddate" HeaderText="Project Enddate"/>
                        <asp:BoundField  DataField="Sponsor" HeaderText="Sponsor"/>
                        <asp:BoundField  DataField="Acronym" HeaderText="Acronym"/>
                        <asp:BoundField  DataField="Reason" HeaderText="Description"/>
         
                  </Columns>
        </asp:GridView>     
                        <asp:SqlDataSource ID="InitialPrInfo" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            OnSelecting="_InitialProjectSelecting" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title , project.startdate,project.enddate, project.reason, project.sponsor,project.acronym FROM project WHERE project.idProject = @idproject">
             <SelectParameters>
             <asp:Parameter Name = "idproject" />
             </SelectParameters>
                        </asp:SqlDataSource>         





            <asp:Label ID="LblInvestigators"   CssClass="Label"  runat="server" Text="Investigators"></asp:Label>
    <asp:GridView ID="grd_General_Partner" runat="server" DataSourceID="InitialSelectPartners" AutoGenerateColumns="False"
          CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"
            CellPadding="4" ForeColor="#52463F" HorizontalAlign="Center" Visible="False" 
                            Font-Size="0.85em" >
            <RowStyle BackColor="#EFF3FB" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1"  ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
         		 		    <Columns>
                        <asp:BoundField  DataField="PersonName" HeaderText="Person Name"/>
                        <asp:BoundField DataField="role" HeaderText="Role"/>
			    <asp:BoundField  DataField="Acronym" HeaderText="Institution Acronym"/>			    		    		
				    <asp:BoundField  DataField="Name" HeaderText="Institution Name"/>
                        </Columns>
        </asp:GridView>     
                        <asp:SqlDataSource ID="InitialSelectPartners" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            OnSelecting="_InitialProjectSelecting" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT personnel.Name as PersonName, project_has_personnel.role, Institution.Acronym, Institution.Name FROM personnel INNER JOIN project_has_personnel ON personnel.idPersonnel = project_has_personnel.Personnel_idPersonnel  INNER JOIN institution on personnel.Institution_idInstitution = Institution.idinstitution WHERE (project_has_personnel.Project_idProject = @idproject)">
             <SelectParameters>
             <asp:Parameter Name = "idproject" />
             </SelectParameters>
                        </asp:SqlDataSource>            
     <br />   
     
      <asp:Label ID="LblInfrastructure"   CssClass="Label"  runat="server" Text="Infrastructure"></asp:Label> 
            <asp:GridView ID="grd_General_Infra" runat="server" CellPadding="4" AutoGenerateColumns="False"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"
                            DataSourceID="InitialSelectInfrastructure" Font-Size="0.85em" 
                            ForeColor="#52463F" GridLines="None" HorizontalAlign="Center" Visible="False">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                         		    <Columns>
                        <asp:BoundField  DataField="Infrastructure" HeaderText="Infrastructure"/>
			    <asp:BoundField  DataField="Resource" HeaderText="Resource"/>			    		    			
			    <asp:BoundField DataField="location" HeaderText="Location"/>
			    <asp:BoundField visible="false" DataField="LocalId" HeaderText="LocalId"/>
                        </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="InitialSelectInfrastructure" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            OnSelecting="_InitialProjectSelecting" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT location.Infrastructure, location.Resource, location.location FROM location INNER JOIN project_has_location ON location.idLocation = project_has_location.Location_idLocation WHERE (project_has_location.Project_idProject = @idproject)">
                            <SelectParameters>
                                <asp:Parameter Name="idproject" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        
        </div>
        <div id="DetailedInfo" class="tab_content">
           <!--Content-->
        
          
    <asp:Menu ID="ProjectMenu" runat="server" StaticBottomSeparatorImageUrl="~/images/icon_separator.gif"  
         Orientation="Horizontal" OnMenuItemClick="ProjectMenu_MenuItemClick">
       <StaticSelectedStyle CssClass="menuitemClicked"/>
       <StaticMenuItemStyle CssClass="menuitem" />
    <Items>
        <asp:MenuItem Text="Project Level" value="Project"   ></asp:MenuItem>
        <asp:MenuItem Text="Specimen Level" value="Specimen"    ></asp:MenuItem>
        <asp:MenuItem Text="Experiment Level" value="Experiment"   ></asp:MenuItem>
        <asp:MenuItem Text="Computation Level" value = "Computation" ></asp:MenuItem>
        <asp:MenuItem Text="Signal Level" value="Signal"   ></asp:MenuItem>
    </Items>
    </asp:Menu>  <br />
           <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="5">        
                    <asp:View ID="SignalView" runat="server">
                    <h4 ><a class="expanded"> &nbsp;  &nbsp; &nbsp;Signal Data</a></h4>
                 <div style="float:left; width:100%">
                   <div  runat="server" id="DivSignalDetail"></div>
                        <asp:GridView ID="GridView17" runat="server" CellPadding="4" AutoGenerateColumns="False"  OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"
                            DataSourceID="SelectSignal" ForeColor="#333333" GridLines="None" 
                            Font-Size="0.85em"  HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                            <asp:BoundField  Visible="false" DataField="CompExp_idCompExp" HeaderText="CompExpID"/>
                        <asp:BoundField  DataField="SignaLabel" HeaderText="SignaLabel"/>
                        <asp:BoundField  DataField="Attribute" HeaderText="Attribute"/>
                        <asp:BoundField  DataField="PhysicalQ" HeaderText="PhysicalQ"/>
                        <asp:BoundField  DataField="Type" HeaderText="Type"/>
                        <asp:BoundField  DataField="Location" HeaderText="Location"/>
                        <asp:BoundField  DataField="Unit" HeaderText="Unit"/>
                       <asp:BoundField  DataField="repetition" HeaderText="Repetition"/>
                        <asp:BoundField Visible="false"  DataField="LocalId" HeaderText="LocalId"/>
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectSignal" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT CompExp_idCompExp, SignaLabel, Attribute, PhysicalQ, Type, Location, Unit, repetition, LocalId FROM signalresult WHERE (idSignalResult = @idsignal)"
                            OnSelecting="_SignalSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idsignal" />
             </SelectParameters>
                        </asp:SqlDataSource>
                       
                        </div>
                      
                    </asp:View>
                    <asp:View ID="ExpCompView" runat="server">
                    </asp:View>
                    <asp:View ID="ProjectView" runat="server">



                    <h4 id="H4PrData" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Project data</a></h4>
                       <div style="float:left; width:100%">
                        <div  runat="server" id="DivDetailPr"></div>
                        <asp:GridView ID="GrdViewDetPrData" runat="server" CellPadding="4" AutoGenerateColumns="False"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                            DataSourceID="SelectProject" ForeColor="#333333" GridLines="None" 
                            Visible="False"  Font-Size="0.85em"  HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                    		    <Columns>
                    		            <asp:BoundField  DataField="Laboratory_Name" HeaderText="Laboratory Name"/>		    		    			
			                            <asp:BoundField DataField="Title" HeaderText="Title"/>
			                            <asp:BoundField DataField="Acronym" HeaderText="Acronym"/>
    			                        <asp:BoundField DataField="Sponsor" HeaderText="Sponsor"/>
    			                        <asp:BoundField DataField="MainFocus" HeaderText="Project Main Focus"/>
    			                        <asp:BoundField DataField="Reason" HeaderText="Summary"/>
			                            <asp:BoundField  DataField="StartDate" HeaderText="StartDate"/>
    			                        <asp:BoundField DataField="EndDate" HeaderText="EndDate"/>
    			                        <asp:BoundField DataField="Status" HeaderText="Status"/>
    			                        <asp:BoundField visible="false" DataField="LocalId" HeaderText="LocalId"/>
                                    </Columns>

                        </asp:GridView>
                        
                        <asp:SqlDataSource ID="SelectProject" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT laboratory.Name as Laboratory_Name, project.StartDate, project.Title, project.EndDate, project.Reason, project.Acronym, project.Sponsor, project.LocalId, project.Status, Project.MainFocus FROM project, laboratory WHERE ( idProject = @idproject and project.laboratory_idlaboratory  = laboratory.idlaboratory )"
                            OnSelecting= "_ProjectSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idproject" />
             </SelectParameters>
                        </asp:SqlDataSource>
            
                        </div>
                        
                        
                        <h4 id="H4PrInvestigator" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Project Investigators</a></h4>
                       <div style="float:left; width:100%">
                        <div  runat="server" id="DivpRrPartners"></div>
          <asp:GridView ID="GrdViewDetPrInvest" runat="server" DataSourceID="SelectPartners" AutoGenerateColumns="False"
          CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1" 
            CellPadding="4" ForeColor="#333333" HorizontalAlign="Center" Visible="False" 
                             Font-Size="0.85em" >
            <RowStyle BackColor="#EFF3FB" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
         		 		    <Columns>
         		 		       
                                <asp:BoundField  DataField="PersonnelName" HeaderText="Personnel Name"/>
		                        <asp:BoundField  DataField="Role" HeaderText="role"/>			    		    		
			                    <asp:BoundField DataField="InstitutionName" HeaderText="Institution Name"/>
			                    <asp:BoundField DataField="InstitutionAcronym" HeaderText="Institution Acronym"/>
            		    </Columns>

        </asp:GridView>            
                        <asp:SqlDataSource ID="SelectPartners" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            OnSelecting="_ProjectSelecting" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT personnel.Name as PersonnelName, project_has_personnel.role, Institution.name as InstitutionName, Institution.Acronym as InstitutionAcronym FROM personnel INNER JOIN project_has_personnel ON personnel.idPersonnel = project_has_personnel.Personnel_idPersonnel inner join institution on institution.idinstitution = personnel.institution_idinstitution WHERE (project_has_personnel.Project_idProject = @idproject)">
             <SelectParameters>
             <asp:Parameter Name = "idproject" />
             </SelectParameters>
                        </asp:SqlDataSource>
              
                       </div>


                        <h4 id="H4PrInfrstr" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Name &amp; Place of the Infrastructure and facility used</a></h4>
                        
                        <div style="float:left; width:100%">
                        <div  runat="server" id="DivPrInfr"></div>
                        <asp:GridView ID="GrdViewDetPrInfrastr" runat="server" CellPadding="4" AutoGenerateColumns="False" OnDataBound="GridView1_DataBound1" 
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"
                            DataSourceID="SelectInfrastructure" Font-Size="0.85em"   
                            ForeColor="#333333" GridLines="None" HorizontalAlign="Center" Visible="False">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                         		    <Columns>
                         		                                 

                        <asp:BoundField  DataField="Infrastructure" HeaderText="Infrastructure"/>
			    <asp:BoundField  DataField="Resource" HeaderText="Resource"/>			    		    			
			    <asp:BoundField DataField="location" HeaderText="Location"/>
			    <asp:BoundField visible="false" DataField="LocalId" HeaderText="LocalId"/>
                        </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectInfrastructure" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            OnSelecting="_ProjectSelecting" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT location.Infrastructure, location.Resource, location.location, location.LocalId FROM location INNER JOIN project_has_location ON location.idLocation = project_has_location.Location_idLocation WHERE (project_has_location.Project_idProject = @idproject)">
                            <SelectParameters>
                                <asp:Parameter Name="idproject" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        </div>
                       
                <h4 id="H4PrDoc" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Project Documents</a></h4>
               <div style="float:left; width:100%">
              <div  runat="server" id="DivPrDoc"></div>
                    <asp:GridView ID="GrdViewDetPrDoc" runat="server" CellPadding="4"   OnRowCommand="OnRowCommand"  OnDataBound="GridView1_DataBound1"  
                    CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"
                        DataSourceID="SelectProjDocs" ForeColor="#333333" GridLines="None" 
                       Visible="False"  Font-Size="0.85em" HorizontalAlign="Center" AutoGenerateColumns="False">
                        <RowStyle BackColor="#EFF3FB" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                         <Columns>
                                                      
                                <asp:BoundField visible="false" DataField="idProjectReport" HeaderText="idProjectReport"/>
                                <asp:BoundField  DataField="Title" HeaderText="Title"/>
                                <asp:BoundField  DataField="Author" HeaderText="Author"/>
                                <asp:BoundField  DataField="abstract" HeaderText="Abstract"/>
                                <asp:BoundField visible="false"  DataField="LocalId" HeaderText="LocalId"/>
                                <asp:BoundField  DataField="docdate" HeaderText="Date"/>
                                <asp:BoundField  DataField="role" HeaderText="Role"/>
                                <asp:BoundField  DataField="scope" HeaderText="Scope"/>
                                <asp:BoundField  DataField="format" HeaderText="Format"/>
                           
                                </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SelectProjDocs" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                        ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                        SelectCommand="SELECT idprojectreport, Title, Author, abstract, docdate, role, Concat_ws(',',concat_ws(' ',Round(Size/1024,2),'KB') ,format) as Format, scope, localID FROM projectreport WHERE (Project_idProject = @idproject)"
                         OnSelecting= "_ProjectSelecting">
                         <SelectParameters>
                                        <asp:Parameter Name="idproject" />
                                    </SelectParameters>
                    </asp:SqlDataSource>
                    </div>
                        
            </asp:View>   
            <asp:View ID="SpecimenView" runat="server"></asp:View>
            <asp:View ID = "LabView" runat ="server"></asp:View>   	 
		    <asp:View ID = "InitialView" runat ="server"></asp:View>   
		    <asp:View ID="SpecimenLevelView" runat="server">

                    <h4 id="H4SpecData" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp; Specimen data</a></h4>
                    <div style="float:left; width:100%">
                     <div  runat="server" id="Div3"></div>
                    <asp:GridView ID="GridSpecLevelSpecData" runat="server" DataSourceID="SelectSpecAll" AutoGenerateColumns="False"  OnRowDataBound="SpecLevel_RowDataBound"
                     CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                            CellPadding="4" ForeColor="#333333" GridLines="None" 
                            Font-Size="0.85em" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                         		    <Columns>
                         		                      
                    	        <asp:BoundField  DataField="Title" HeaderText="Project Title"/>
			                    <asp:BoundField  DataField="Name" HeaderText="Specimen Name"/>			    		
		                        <asp:BoundField  DataField="Width" HeaderText="Max Width(m)"/>			    		
                                <asp:BoundField  DataField="Length" HeaderText="Max Length(m)"/>			    		
                                <asp:BoundField  DataField="Height" HeaderText="Max Height(m)"/>			    		
                                <asp:BoundField  DataField="Depth" HeaderText="Max Depth(m)"/>	
                                <asp:BoundField  DataField="SpecimenMass" HeaderText="SpecimenMass(kg)"/>			    					    		
                                <asp:BoundField Visible="false"  DataField="LocalId" HeaderText="LocalId"/>			    					    		
                                <asp:BoundField visible="false"  DataField="idSpecimen" HeaderText="idSpecimen"/>    		
                        </Columns>
                        </asp:GridView>
                        
                        <asp:SqlDataSource ID="SelectSpecAll" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT Project.Title, specimen.Project_idProject, specimen.Name, specimen.Width, specimen.Length, specimen.Height, specimen.Depth, specimen.SpecimenMass, specimen.LocalId, specimen.idSpecimen FROM specimen, project WHERE (project.idproject = @idproject AND specimen.project_idproject = project.idproject)"
                             OnSelecting = "_LevelAllSelecting">
                        <SelectParameters>
            <asp:Parameter Name = "idproject" />
            </SelectParameters>
                        </asp:SqlDataSource>
                    
                    </div>
                    
                        
                        <h4 id="H4SpecStrElem" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp; Structural elements</a></h4>
                        <div style="float:left; width:100%">
                         <div  runat="server" id="Div4"></div>
                        <asp:GridView ID="GridSpecLevelStrElemData" runat="server" CellPadding="4" AutoGenerateColumns="False"  OnRowDataBound="SpecLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                            DataSourceID="SelectStrElemSpecLevel" ForeColor="#333333" GridLines="None" 
                            Font-Size="0.85em" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                         		    <Columns>
                         		                      
                         		        <asp:BoundField  DataField="projecttitle" HeaderText="Project Title"/>
                         		        <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                    	                <asp:BoundField  DataField="StructuralElementName" HeaderText="Structural Element(s)"/>		    		
			                            <asp:BoundField  DataField="Type" HeaderText="Type"/>	
			                            <asp:BoundField  DataField="materialDescription" HeaderText="Material Description"/>		    				
                                    </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectStrElemSpecLevel" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title as projecttitle, specimen.Name As SpecName, structuralelement.Name AS StructuralElementName, structuralelement.materialDescription, structuralelement.Type FROM structuralelement, specimen, project WHERE (structuralelement.Specimen_idSpecimen= specimen.idspecimen and specimen.project_idproject = project.idproject and project.idproject = @idproject)"
                             OnSelecting = "_LevelAllSelecting">
                             <SelectParameters>
                                <asp:Parameter Name = "idproject" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        </div>

                        <h4 id="H4SpecStrElemMat" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp; Structural Elements and it&#39;s materials</a></h4>
                        <div style="float:left; width:100%">
                         <div  runat="server" id="Div5"></div>
                        <asp:GridView ID="GridSpecLevelStrElemMaterial" runat="server" CellPadding="4" AutoGenerateColumns="False"  OnRowDataBound="SpecLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                            DataSourceID="SelectStrElemMaterialSpecLevel" ForeColor="#333333" GridLines="None" 
                             Font-Size="0.85em" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                         		    <Columns>
                         		                                 
                         		     <asp:BoundField  DataField="projecttitle" HeaderText="Project Title"/>
                         		     <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                    	            <asp:BoundField  DataField="StructuralElement" HeaderText="StructuralElement"/>
		                    	    <asp:BoundField  DataField="materialName" HeaderText="MaterialName"/>			    		
                                </Columns>

                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectStrElemMaterialSpecLevel" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title as projecttitle,Specimen.name as specName, structuralelement.Name AS StructuralElement, material.Name As materialName FROM material INNER JOIN structuralelement_has_material ON material.idMaterial = structuralelement_has_material.Material_idMaterial INNER JOIN structuralelement ON structuralelement_has_material.StructuralElement_idStructuralElement = structuralelement.idStructuralElement INNER JOIN specimen on specimen.idspecimen = structuralelement.Specimen_idSpecimen INNER JOIN project on project.idproject = specimen.project_idproject WHERE (project.idproject = @idproject)"
                             OnSelecting = "_LevelAllSelecting">
                             <SelectParameters>
            <asp:Parameter Name = "idproject" />
            </SelectParameters>
                        </asp:SqlDataSource>
                  
                        </div>

                        <h4  id="H4StrlElem_Mat_Nom_Prop" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp; Structural elements,Materials and material nominal Properties</a></h4>
                       <div style="float:left; width:100%">
                         <div  runat="server" id="Div6"></div>
                        <asp:GridView ID="GridSpecLevelStrElemMatNomProp" runat="server" CellPadding="4" AutoGenerateColumns="False"  OnRowDataBound="SpecLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                             Font-Size="0.85em" 
                            GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>                             
                                <asp:BoundField  DataField="projecttitle" HeaderText="Project Title"/>
                         	    <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                    	        <asp:BoundField  DataField="StructuralElement" HeaderText="Structural Element"/>
			                    <asp:BoundField  DataField="materialName" HeaderText="material Name"/>	
			                    <asp:BoundField  DataField="materialPropertyName" HeaderText="Property Name"/>			    		
                                <asp:BoundField  DataField="materialpropertyValue" HeaderText="Property Value"/>			    		
                                <asp:BoundField  DataField="materialpropertyUnit" HeaderText="Property Unit"/>
                                <asp:BoundField  DataField="observations" HeaderText="Observations"/>			    		
                                <asp:BoundField  DataField="materialCurve" HeaderText="Material Curve"/>	    					    		
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectStrElemMatNomPropSpecLevel" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title as projectTitle , specimen.name as SpecName, structuralelement.Name as structuralelement, material.Name AS materialName,materialnominalproperty.materialpropertyName as materialPropertyName,materialnominalproperty.materialpropertyValue,materialnominalproperty.materialpropertyUnit,materialnominalproperty.materialCurve,materialnominalproperty.observations FROM materialnominalproperty LEFT OUTER JOIN material ON materialnominalproperty.materialID = material.idMaterial LEFT OUTER JOIN structuralelement_has_material ON material.idMaterial = structuralelement_has_material.Material_idMaterial LEFT OUTER JOIN structuralelement ON structuralelement_has_material.StructuralElement_idStructuralElement = structuralelement.idStructuralElement LEFT OUTER JOIN specimen on specimen.idspecimen = structuralelement.Specimen_idSpecimen LEFT OUTER JOIN project on project.idproject = specimen.project_idproject  WHERE (project.idproject = @idproject)"
                             OnSelecting = "_LevelAllSelecting">
                       <SelectParameters>
            <asp:Parameter Name = "idproject" />
            </SelectParameters>
                        </asp:SqlDataSource>
                       
                        </div>

                        <h4 id="H4StrlElem_Mat_ActualProp" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp; Structural elements,Materials and material Actual Properties</a></h4>
                        <div style="float:left; width:100%">
                         <div  runat="server" id="Div7"></div>
                        <asp:GridView ID="GridSpecLevelStrElemMatActProp" runat="server" CellPadding="4" AutoGenerateColumns="False"  OnRowDataBound="SpecLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                            DataSourceID="SelectStrElemMatActPropSpecLevel" ForeColor="#333333" 
                             Font-Size="0.85em" 
                            GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                                        
                             <asp:BoundField  DataField="projecttitle" HeaderText="Project Title"/>
                         	 <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                    	    <asp:BoundField  DataField="structuralelementName" HeaderText="Structural Element"/>
			                <asp:BoundField  DataField="materialName" HeaderText="Material Name"/>
			                <asp:BoundField  DataField="materialPropertyName" HeaderText="Property Name"/>	
			                <asp:BoundField  DataField="samples" HeaderText="Samples"/>	  		
                    	    <asp:BoundField  DataField="materialpropertyValue" HeaderText="Property Value"/>			    		
                    	    <asp:BoundField  DataField="unit" HeaderText="Unit"/>
			                <asp:BoundField  DataField="observations" HeaderText="Observations"/>				    		
                    	    <asp:BoundField visible="false" DataField="ActualPropertyLocalID" HeaderText="ActualPropertyLocalID"/>			    		
                    	    <asp:BoundField visible="false" DataField="structuralElementLocalID" HeaderText="structuralElementLocalID"/>			    		            
                    	    </Columns>

                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectStrElemMatActPropSpecLevel" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT Project.Title as projectTitle, specimen.name as SpecName, structuralelement.Name as structuralelementName, material.Name AS materialName, materialactualprop.properties As materialPropertyName, materialactualprop.value as materialPropertyValue, materialactualprop.unit,  materialactualprop.samples, materialactualprop.LocalId AS ActualPropertyLocalID, structuralelement.LocalId as structuralElementLocalID ,materialactualprop.observations FROM
project, specimen,structuralelement,structuralelement_has_material, materialactualprop,material
WHERE
project.idproject = specimen.project_idproject AND 
structuralelement.specimen_idspecimen = specimen.idspecimen AND  
structuralelement_has_material.StructuralElement_idStructuralElement = structuralelement.idStructuralElement  AND
structuralelement_has_material.Material_idMaterial = material.idMaterial AND
structuralelement_has_material.StructuralElement_idStructuralElement = materialactualprop.StructuraElement_idStructuralElement AND
structuralelement_has_material.Material_idMaterial = materialactualprop.Material_idMaterial AND
project.idproject= @idproject"
                            OnSelecting = "_LevelAllSelecting">
                       <SelectParameters>
            <asp:Parameter Name = "idproject" />
            </SelectParameters>
                        </asp:SqlDataSource>
                        </div>
            
                        <h4 id="H4SpecDoc" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp; Specimen Documents</a></h4>
                        <div style="float:left; width:100%">
                         <div  runat="server" id="Div8"></div>
                        <asp:GridView ID="GridSpecLevelSpecDocs" runat="server" CellPadding="4" AutoGenerateColumns="False"  OnRowDataBound="SpecLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"
                            DataSourceID="SelectSpecDocSpecLevel" ForeColor="#333333" GridLines="None"  OnDataBound="GridView1_DataBound1"
                            Font-Size="0.85em" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                         		 <Columns>
                         		                             
                                    <asp:BoundField  DataField="projecttitle" HeaderText="Project Title"/>
                         	        <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
			                        <asp:BoundField  DataField="DocTitle" HeaderText="Document Name"/>
			                        <asp:BoundField  DataField="Author" HeaderText="Author"/>	
			                        <asp:BoundField  DataField="Abstract" HeaderText="Abstract"/>
			                        <asp:BoundField  DataField="DocDate" HeaderText="Date"/>
			                        <asp:BoundField  DataField="role" HeaderText="Role"/>
                                    <asp:BoundField  DataField="scope" HeaderText="Scope"/>	
                                    <asp:BoundField DataField="Format" HeaderText="Format"/>	    		  		
                    	            <asp:BoundField  DataField="SizeKb" HeaderText="Size(Kb)"/>			
                    	            <asp:BoundField DataField="Format" HeaderText="Format"/>
                    	            <asp:BoundField visible="false"  DataField="LocalId" HeaderText="LocalId"/>		
			    		          </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectSpecDocSpecLevel" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title as projectTitle ,specimen.name as SpecName, specimenreport.title as DocTitle, specimenreport.Author, specimenreport.Abstract, specimenreport.LocalId,  Round(specimenreport.Size/1024,2) as SizeKb, specimenreport.Format, specimenreport.role, specimenreport.scope, specimenreport.docdate FROM specimenreport,project,specimen WHERE (project.idproject= @idproject AND specimen.idspecimen = specimenreport.specimen_idspecimen AND project.idproject = specimen.project_idproject)"
                            OnSelecting = "_LevelAllSelecting">
                       <SelectParameters>
            <asp:Parameter Name = "idproject" />
            </SelectParameters>
                        </asp:SqlDataSource>
                    
                        </div>

                        <h4 id="H4SpecImg" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp; Specimen Images</a></h4>
                        <div style="float:left; width:100%">
                         <div  runat="server" id="Div9"></div>
                        <asp:GridView ID="GridSpecLevelSpecIMGs" runat="server" CellPadding="4" AutoGenerateColumns="False"  OnRowDataBound="SpecLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                            DataSourceID="SelectSpecLevelSpecIMGs" ForeColor="#333333" GridLines="None" 
                            Font-Size="0.85em" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                         		    <Columns>
                         		                                
                         		        <asp:BoundField  DataField="projecttitle" HeaderText="Project Title"/>
                         	            <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
			                            <asp:BoundField  DataField="Name" HeaderText="Name"/>	
			                            <asp:BoundField  DataField="CreationDate" HeaderText="Creation Date"/>			    		
                                   	    <asp:BoundField  DataField="role" HeaderText="Role"/>			    		
                                	    <asp:BoundField visible="false"  DataField="LocalId" HeaderText="LocalId"/>
			                            <asp:BoundField DataField="Author" HeaderText="Author"/>
			                            <asp:BoundField DataField="Format" HeaderText="Format"/>
			                            <asp:BoundField DataField="Size" HeaderText="Size"/>
			    		             </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectSpecLevelSpecIMGs" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title as projecttitle, specimen.name as SpecName ,specimenpictures.Name as Name, specimenpictures.CreationDate as CreationDate, specimenpictures.role, specimenpictures.LocalId, specimenpictures.author,specimenpictures.format,Round(specimenpictures.size/1024,2) as Size,specimenpictures.summary FROM specimenpictures,specimen,project WHERE (project.idproject = specimen.project_idproject and specimen.idSpecimen = specimenpictures.Specimen_idSpecimen and project.idproject = @idproject)"
                            OnSelecting = "_LevelAllSelecting">
                            <SelectParameters>
            <asp:Parameter Name = "idproject" />
            </SelectParameters>
                        </asp:SqlDataSource>
                        </div>
                        
                        <h4 id="H4SpecScaling" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp; Specimen scaling table</a></h4>
                      <div style="float:left; width:100%">
                         <div  runat="server" id="Div10"></div>
                        <asp:GridView ID="GridSpecLevelSpecScaling" runat="server" CellPadding="4" AutoGenerateColumns="False"  OnRowDataBound="SpecLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                        Font-Size="0.85em" 
                            DataSourceID="SelectSpecLevelScaling" ForeColor="#333333" GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                         		  <Columns>
                         		                             
                         		     <asp:BoundField  DataField="projecttitle" HeaderText="Project Title"/>
                         	         <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
			                         <asp:BoundField  DataField="prototypeModel_ratio" HeaderText="Prototype-Model Ratio"/>			    		
                    	             <asp:BoundField  DataField="scaledPropertyName" HeaderText="Scaled Property Name"/>			    		
                   	                  <asp:BoundField visible="false"  DataField="LocalId" HeaderText="LocalId"/>
		                           </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectSpecLevelScaling" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title as Projecttitle, specimen.name as SpecName, similitude.prototypeModel_ratio, similitude.scaledPropertyName, similitude.LocalId FROM project,specimen, similitude WHERE (project.idproject = specimen.project_idproject and similitude.Specimen = specimen.idspecimen and project.idproject = @idproject)"
                            OnSelecting = "_LevelAllSelecting">
                            <SelectParameters>
            <asp:Parameter Name = "idproject" />
            </SelectParameters>
                        </asp:SqlDataSource>
                        </div>
                        
                        
                    </asp:View>
                    <asp:View ID="CompExpLevelView" runat="server">
                    <h4 id="H4ExpCompData" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Experiment/Computation</a></h4>        
                    <div style="float:left; width:100%">
                    <div  runat="server" id="Div11"></div>
                        <asp:GridView ID="GridCompExpLevelData" runat="server" CellPadding="4"  AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"
                            DataSourceID="SelectCompExpLevelAll" ForeColor="#333333" GridLines="None"  OnDataBound="GridView1_DataBound1"
                             Font-Size="0.85em" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                               <Columns>
                                                          
                                    <asp:BoundField  DataField="PrTitle" HeaderText="Project Name"/>
                                    <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                                     <asp:BoundField  DataField="Name" HeaderText="Name"/>
                                    <asp:BoundField  DataField="creationDate" HeaderText="Time Stamp"/>
                                    <asp:BoundField  DataField="ExpComptype" HeaderText="ExpComp Type"/>
                                   
                                    <asp:BoundField  DataField="Repetition" HeaderText="Repetition"/>
                                    
                                    <asp:BoundField  DataField="loadingCoef" HeaderText="Loading Coefficient"/>
                                    <asp:BoundField  DataField="PeakExcitUnit" HeaderText="Peak Excitation Unit"/>
                                    <asp:BoundField  DataField="PeakExcitValue" HeaderText="Peak Excitation Value"/>
                                    <asp:BoundField  DataField="type" HeaderText="type"/>
                                    <asp:BoundField  Visible="false" DataField="Specimen_idSpecimen" HeaderText="SpecimenID"/>
                                    <asp:BoundField Visible="false"  DataField="LocalId" HeaderText="LocalId"/>
                            </Columns>  
                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectCompExpLevelAll" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title AS PrTitle, specimen.Name AS SpecName, compexp.Specimen_idSpecimen, compexp.Name, compexp.creationDate, compexp.Repetition, compexp.PeakExcitUnit, compexp.PeakExcitValue, compexp.LocalId, compexp.Type, compexp.loadingCoef, compexp.ExpCompType FROM project,specimen,compexp WHERE (project.idproject = @idproject and project.idproject = specimen.project_idproject AND specimen.idspecimen = compexp.specimen_idspecimen and compexp.type= @type )"
                             OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idproject" />
             <asp:Parameter Name = "type" />
             </SelectParameters>
                        </asp:SqlDataSource>
                        </div>
                        
                        <h4 id="H4ExpCompPerson" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Experiment Agents(Personnel)</a></h4>
                        <div style="float:left; width:100%">
                        <div  runat="server" id="Div12"></div>
                        <asp:GridView ID="GridCompExpLevelPerson" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"  OnDataBound="GridView1_DataBound1"
                         Font-Size="0.85em" 
                            DataSourceID="SelectCompExpLevelPerson" ForeColor="#333333" GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                                        
                            <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                            <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                            <asp:BoundField  DataField="PersonName" HeaderText="Name"/>
                            <asp:BoundField  DataField="InstitutionAcronym" HeaderText="Institution Acronym"/>
                            <asp:BoundField  DataField="InstitutionName" HeaderText="Institution Name"/>
                           </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectCompExpLevelPerson" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title as PrTitle, specimen.Name as SpecName, personnel.Name as PersonName, Institution.Acronym as InstitutionAcronym, Institution.Name as InstitutionName FROM Project INNER JOIN specimen on project.idproject = specimen.project_idproject INNER JOIN compExp ON specimen.idspecimen = compExp.specimen_idspecimen INNER JOIN comp_has_personnel  on comp_has_personnel.compexp_idcompexp = compexp.idcompexp INNER JOIN personnel ON comp_has_personnel.Personnel_idPersonnel = personnel.idPersonnel INNER JOIN Institution on Institution.idinstitution = personnel.institution_idinstitution WHERE (project.idproject = @idproject and compexp.type= @type)"
                            OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idproject" />
             <asp:Parameter Name = "type" />
             </SelectParameters>
                        </asp:SqlDataSource>
                        </div>
                       
                           <h4 id="H4ExpCompOLS" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Original Loading Signals</a></h4>
                      <div style="float:left; width:100%">
                      <div  runat="server" id="Div14"></div>
                        <asp:GridView ID="GridCompExpLevelOLS" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                            DataSourceID="SelectCompExpLevelOLS" ForeColor="#333333" 
                            Font-Size="0.85em" 
                            GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                                         
                        <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                        <asp:BoundField  DataField="specName" HeaderText="Specimen Name"/>
                        <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>
                        <asp:BoundField  DataField="originalLoadingName" HeaderText="Original Loading Name"/>
                        <asp:BoundField  DataField="Nature" HeaderText="Nature"/>
                        <asp:BoundField  DataField="Source" HeaderText="Source"/>
                        <asp:BoundField  DataField="PeakExcitationUnit" HeaderText="Peak Excitation Unit"/>
                         <asp:BoundField  DataField="PeakExcitationValue" HeaderText="Peak Excitation Value"/>
                          
                        <asp:BoundField Visible="false" DataField="LocalId" HeaderText="LocalId"/>
                           </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectCompExpLevelOLS" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title AS PrTitle, specimen.name AS specName, compexp.Name AS CompExpName, originalloading.Name AS originalLoadingName, originalloading.Nature, originalloading.Source,  originalloading.PeakExcitationUnit, originalloading.PeakExcitationValue, originalloading.LocalId FROM project INNER JOIN specimen ON specimen.project_idproject = project.idproject INNER JOIN CompExp on specimen.idspecimen = compexp.specimen_idspecimen INNER JOIN compexp_has_dlc on compexp_has_dlc.compexp_idcompexp = compexp.idcompexp INNER JOIN dlc ON dlc.iddlc= compexp_has_dlc.dlc_iddlc INNER JOIN nominalloading_originalloading ON nominalloading_originalloading.NominalLoading = dlc.iddlc INNER JOIN originalloading ON originalloading.idoriginalloading = nominalloading_originalloading.originalLoading  WHERE (project.idproject = @idproject and compexp.type= @type)"
                            OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idproject" />
             <asp:Parameter Name = "type" />
             </SelectParameters>
                        </asp:SqlDataSource>
               
                        </div>

                <h4 id="H4ExpCompOLS_Signal" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp; Original Loading Signals Attributes</a></h4>
                      <div style="float:left; width:100%">
                      <div  runat="server" id="Div25"></div>
                    <asp:GridView ID="GridCompExpLevelOLS_Signals" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"  OnDataBound="GridView1_DataBound1"
                            DataSourceID="SelectCompExpLevelOLS_Signal" ForeColor="#333333" 
                            Font-Size="0.85em" 
                            GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                                         
                                <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                                <asp:BoundField  DataField="specName" HeaderText="Specimen Name"/>
                                <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>
                                <asp:BoundField  DataField="DLCName" HeaderText="DLC Name"/>
                                <asp:BoundField  DataField="OLSName" HeaderText="OLS Name"/>
                                <asp:BoundField  DataField="Sig_Name" HeaderText="Signal Name"/>
                                <asp:BoundField  DataField="Sig_Attr" HeaderText="Signal Attribute"/>
                                <asp:BoundField  DataField="Sig_PQ" HeaderText="Signal Physical Quantity"/>
                                <asp:BoundField  DataField="Sig_Type" HeaderText="Signal Type"/>
                                <asp:BoundField  DataField="Sig_Unit" HeaderText="Signal Unit"/>
                                <asp:BoundField  DataField="Sig_Location" HeaderText="Signal Location"/>
                                
                                <asp:BoundField  Visible="false" DataField="LocalId" HeaderText="LocalId"/>
                           </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectCompExpLevelOLS_Signal" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title AS PrTitle, specimen.Name AS specName, CompExp.Name AS CompExpName, dlc.Name AS DLCName, originalloading.name as OLSName,inputsignal.Label as Sig_Name,inputsignal.Attribute as  Sig_Attr,inputsignal.PhysicalQ as Sig_PQ,inputsignal.type as Sig_Type,inputsignal.Unit as Sig_Unit,inputsignal.Location as Sig_Location,inputsignal.repetition FROM project, specimen, compexp, dlc, originalloading,compexp_has_dlc, nominalloading_originalloading,inputsignal WHERE (project.idproject = @idproject and project.idproject = specimen.project_idproject and specimen.idspecimen = compexp.specimen_idspecimen and compexp_has_dlc.CompExp_idCompExp= compexp.idcompexp and compexp_has_dlc.dlc_iddlc = dlc.iddlc and nominalloading_originalloading.NominalLoading = dlc.iddlc and nominalloading_originalloading.originalLoading = OriginalLoading.idOriginalLoading and OriginalLoading.signal_idsignal = inputsignal.idInputSignal and compexp.type= @type )"
                            OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idproject" />
             <asp:Parameter Name = "type" />
             </SelectParameters>
                        </asp:SqlDataSource>
                
                         </div>
                       
                       
                          <h4 id="H4ExpCompDLC" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Detailed Loading Characteristics</a></h4>
                      <div style="float:left; width:100%">
                      <div  runat="server" id="Div13"></div>
                    <asp:GridView ID="GridCompExpLevelDLC" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"  OnDataBound="GridView1_DataBound1"
                            DataSourceID="SelectCompExpLevelDLC" ForeColor="#333333" 
                             Font-Size="0.85em" 
                            GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                
                                <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                                <asp:BoundField  DataField="specName" HeaderText="Specimen Name"/>
                                <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>
                                <asp:BoundField  DataField="DLCName" HeaderText="DLC Name"/>
                                <asp:BoundField  DataField="OLSName" HeaderText="OLS Name"/>
                                 <asp:BoundField  DataField="Direction" HeaderText="Direction"/>
                                <asp:BoundField  DataField="loadingCoef" HeaderText="Loading Coefficient(%)"/>
                                <asp:BoundField  DataField="AdditionalParameter" HeaderText="Additional Parameter"/>
                                <asp:BoundField  DataField="notes" HeaderText="Notes"/>
                                <asp:BoundField  Visible="false" DataField="LocalId" HeaderText="LocalId"/>
                           </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectCompExpLevelDLC" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT distinct project.title AS PrTitle, specimen.Name AS specName, CompExp.Name AS CompExpName, dlc.Name AS DLCName, dlc.AdditionalParameter,  dlc.loadingCoef, dlc.Notes, dlc.LocalId, originalloading.name as OLSName,nominalloading_originalloading.DirectionHorizon as direction FROM project, specimen, compexp, dlc, originalloading,compexp_has_dlc, nominalloading_originalloading WHERE (project.idproject = @idproject and project.idproject = specimen.project_idproject and specimen.idspecimen = compexp.specimen_idspecimen and compexp_has_dlc.CompExp_idCompExp= compexp.idcompexp and compexp_has_dlc.dlc_iddlc = dlc.iddlc and nominalloading_originalloading.NominalLoading = dlc.iddlc and nominalloading_originalloading.originalLoading = OriginalLoading.idOriginalLoading and compexp.type= @type )"
                            OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idproject" />
             <asp:Parameter Name = "type" />
             </SelectParameters>
                        </asp:SqlDataSource>
                
                         </div>
                        
                               <h4 id="H4ExpCompInputFile" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Effective Input Files</a></h4>
                      <div style="float:left; width:100%">
                      <div  runat="server" id="Div24"></div>
                    <asp:GridView ID="GridCompExpLevelInputFile" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"  OnDataBound="GridView1_DataBound1"  
                            DataSourceID="SelectCompExpLevelInputFile" ForeColor="#333333" 
                             Font-Size="0.85em" 
                            GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                         
                                <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                                <asp:BoundField  DataField="specName" HeaderText="Specimen Name"/>
                                <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>
                                <asp:BoundField  DataField="DLCName" HeaderText="DLC Name"/>
                                <asp:BoundField  DataField="OLSName" HeaderText="OLS Name"/>
                                <asp:BoundField  DataField="EffectiveInputFile" HeaderText="Effective Input File name"/>
                                <asp:BoundField  DataField="InputSig_Attr" HeaderText="Effective Input File Attribute"/>
                                <asp:BoundField  DataField="InputSig_PQ" HeaderText="Effective Input File Physical Quantity"/>
                                <asp:BoundField  DataField="InputSig_Type" HeaderText="Effective Input File Type"/>
                                <asp:BoundField  DataField="InputSig_Unit" HeaderText="Effective Input File Unit"/>
                                <asp:BoundField  DataField="InputSig_Location" HeaderText="Effective Input File Location"/>
                                
                                 
                        
                                <asp:BoundField  Visible="false" DataField="LocalId" HeaderText="LocalId"/>
                           </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectCompExpLevelInputFile" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT  project.title AS PrTitle, specimen.Name AS specName, CompExp.Name AS CompExpName, dlc.Name AS DLCName, originalloading.name as OLSName,inputsignal.Label as EffectiveInputFile,inputsignal.Attribute as InputSig_Attr,inputsignal.PhysicalQ as InputSig_PQ,inputsignal.type as InputSig_Type,inputsignal.Unit as InputSig_Unit,inputsignal.Location as InputSig_Location,inputsignal.repetition FROM project, specimen, compexp, dlc, originalloading,compexp_has_dlc, nominalloading_originalloading,inputsignal WHERE (project.idproject = @idproject and project.idproject = specimen.project_idproject and specimen.idspecimen = compexp.specimen_idspecimen and compexp_has_dlc.CompExp_idCompExp= compexp.idcompexp and compexp_has_dlc.dlc_iddlc = dlc.iddlc and nominalloading_originalloading.NominalLoading = dlc.iddlc and nominalloading_originalloading.originalLoading = OriginalLoading.idOriginalLoading and nominalloading_originalloading.EffectiveInputFile = inputsignal.idInputSignal and compexp.type= @type  ) "
                            OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idproject" />
             <asp:Parameter Name = "type" />
 
             </SelectParameters>
                        </asp:SqlDataSource>
                
                         </div>
                        
                        
                     
                        <h4 id="H4ExpCompMeshModel" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Mesh Models</a></h4>
                        <div style="float:left; width:100%"> <div  runat="server" id="Div15"></div>
                        <asp:GridView ID="GridCompExpLevelMeshModel" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                            DataSourceID="SelectCompExpLevelMeshModel" ForeColor="#333333" 
                             Font-Size="0.85em" 
                            GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                                        
                            <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                        <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                        <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>
                        <asp:BoundField  DataField="Nonlinearity" HeaderText="Nonlinearity"/>
                        <asp:BoundField  DataField="SymmetryType" HeaderText="SymmetryType"/>
                        <asp:BoundField  DataField="material" HeaderText="Material"/>
                        <asp:BoundField  Visible="false" DataField="LocalId" HeaderText="LocalId"/>
                           </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectCompExpLevelMeshModel" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title AS PrTitle,specimen.name AS SpecName, compexp.name as CompExpName ,meshmodel.Nonlinearity ,meshmodel.SymmetryType, meshmodel.LocalId, material.name as material from project,specimen,compexp, meshmodel,material,meshmodel_has_material WHERE (project.idproject = @idproject AND specimen.project_idproject = project.idproject and compexp.specimen_idspecimen = specimen.idspecimen and meshmodel.CompExp_idcompexp = compexp.idcompexp and compexp.type= @type and meshmodel.idmeshmodel = meshmodel_has_material.meshmodel_idmeshmodel and meshmodel_has_material.material_idmaterial = material.idmaterial)"
                            OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idproject" />
             <asp:Parameter Name = "type" />
             </SelectParameters>
                        </asp:SqlDataSource>
                  
                        </div>
                       
                        <h4 id="H4ExpCompMeshModelDoc" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Mesh Models Documents</a></h4>
                        <div style="float:left; width:100%">
                        <div  runat="server" id="Div16"></div>
                        <asp:GridView ID="GridCompExpLevelMeshModelDoc" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                            DataSourceID="SelectCompExpLevelMeshModelDoc" ForeColor="#333333" 
                            Font-Size="0.85em" 
                            GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                                          
                          <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                        <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                        <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>    
                        <asp:BoundField  DataField="Title" HeaderText="Title"/>
                        <asp:BoundField  DataField="Author" HeaderText="Author"/>
                        <asp:BoundField  DataField="SizeKb" HeaderText="Size(Kb)"/>
                        <asp:BoundField  DataField="Format" HeaderText="Format"/>
                        <asp:BoundField  DataField="Abstract" HeaderText="Abstract"/>
                        <asp:BoundField  DataField="role" HeaderText="Role"/>
                        <asp:BoundField  DataField="scope" HeaderText="Scope"/>
                        <asp:BoundField  DataField="creationdate" HeaderText="Creation Date"/>
                        <asp:BoundField Visible="false"  DataField="LocalId " HeaderText="LocalId "/>
                           </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectCompExpLevelMeshModelDoc" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title AS PrTitle, specimen.Name AS SpecName,CompExp.Name as CompExpName, meshmodeldocuments.Title , meshmodeldocuments.Author, Round(meshmodeldocuments.Size/1024,2) as SizeKb ,meshmodeldocuments.Format, meshmodeldocuments.Abstract,meshmodeldocuments.scope, meshmodeldocuments.role, meshmodeldocuments.creationdate, meshmodel.LocalId from project, specimen,compexp, meshmodel,meshmodeldocuments WHERE (project.idproject = @idproject and project.idproject = specimen.project_idproject AND compexp.specimen_idspecimen = specimen.idspecimen AND meshmodel.CompExp_idcompexp = compexp.idcompexp AND  meshmodeldocuments.MeshModel_IdMeshModel = meshmodel.idMeshModel and compexp.type= @type)"
                            OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idproject" />
             <asp:Parameter Name = "type" />
             </SelectParameters>
                        </asp:SqlDataSource>
                       
                        </div>

                        <h4  id="H4ExpCompMeshModelIMG" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Mesh Models Images</a></h4>
                        <div style="float:left; width:100%">
                        <div  runat="server" id="Div17"></div>
                        <asp:GridView ID="GridCompExpLevelMeshModelIMG" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                            DataSourceID="SelectCompExpLevelMeshModelIMG" ForeColor="#333333" 
                             Font-Size="0.85em" 
                            GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                                         
                              <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                        <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                        <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>
                        <asp:BoundField  DataField="Name" HeaderText="Image Name"/>
                        <asp:BoundField  DataField="PictureDate" HeaderText="Picture Date"/>
                        <asp:BoundField  DataField="role" HeaderText="Role"/>
                        <asp:BoundField  DataField="author" HeaderText="Author"/>
                        <asp:BoundField  DataField="Imgsize" HeaderText="Size"/>
                        <asp:BoundField  DataField="summary" HeaderText="Summary"/>
                        <asp:BoundField  DataField="format" HeaderText="Format"/>
                         <asp:BoundField Visible="false" DataField="ImageLocalID" HeaderText="ImageLocalID"/>
                        <asp:BoundField Visible="false" DataField="MeshModelLocalID" HeaderText="MeshModelLocalID"/>
                           </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectCompExpLevelMeshModelIMG" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title AS PrTitle, specimen.Name AS SpecName, CompExp.Name as CompExpName, meshmodelpicture.Name , meshmodelpicture.PictureDate, meshmodelpicture.role, meshmodelpicture.author ,meshmodelpicture.format, Round(meshmodelpicture.size/1024,3) as imgSize, meshmodelpicture.summary, meshmodelpicture.LocalID as ImageLocalID , meshmodel.LocalId as MeshModelLocalID  from project, specimen, compexp, meshmodel,meshmodelpicture WHERE (project.idproject = @idproject and project.idproject = specimen.project_idproject AND compexp.specimen_idspecimen = specimen.idspecimen AND meshmodel.CompExp_idcompexp = compexp.idcompexp  AND MeshModelPicture.MeshModel_IdMeshModel = meshmodel.idMeshModel AND meshmodel.CompExp_idcompexp = compexp.idcompexp and compexp.type= @type)"
                            OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idproject" />
             <asp:Parameter Name = "type" />
             </SelectParameters>
                        </asp:SqlDataSource>
            
                        </div>
                      
                        <h4 id="H4ExpCompIMGS" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Experiment Images</a></h4>
                       <div style="float:left; width:100%">             
                        <div  runat="server" id="Div18"></div>
                        <asp:GridView ID="GridCompExpLevelIMG" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                            DataSourceID="SelectCompExpLevelIMG"  Font-Size="0.85em" 
                            ForeColor="#333333" GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                                    
                             <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                        <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                        <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>
                        <asp:BoundField  DataField="title" HeaderText="Title"/>
                        <asp:BoundField  DataField="creationDate" HeaderText="Creation Date"/>
                        <asp:BoundField  DataField="role" HeaderText="Role"/>
                        <asp:BoundField  DataField="author" HeaderText="Author"/>
                        <asp:BoundField  DataField="summary" HeaderText="Summary"/>
                        <asp:BoundField  DataField="format" HeaderText="Format"/>
                        <asp:BoundField  DataField="Imgsize" HeaderText="Size"/>
                        <asp:BoundField Visible="false"  DataField="LocalId" HeaderText="LocalId"/>
                       
                           </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectCompExpLevelIMG" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            OnSelecting="_LevelAllSelecting" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title AS PrTitle, specimen.Name AS SpecName, CompExp.Name as CompExpName, compexppictures.title, compexppictures.creationDate,compexppictures.LocalId, compexppictures.role, round(compexppictures.size/1024,2) as ImgSize ,compexppictures.summary,compexppictures.format FROM project, specimen, compexp, compexppictures WHERE (project.idproject = @idproject and project.idproject = specimen.project_idproject AND compexp.specimen_idspecimen = specimen.idspecimen  AND compexppictures.CompExp_idCompExp = compexp.idcompexp and compexp.type= @type)">
                            <SelectParameters>
                                <asp:Parameter Name="idproject" />
                                <asp:Parameter Name = "type" />
                            </SelectParameters>
                        </asp:SqlDataSource>
              
                        </div>

                        <h4 id="H4ExpCompDoc" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Computations/experiment Documents</a></h4>
                        <div style="float:left; width:100%">
                        <div  runat="server" id="Div19"></div>
                        <asp:GridView ID="GridCompExpLevelDOC" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                            DataSourceID="selectcompexpLeveldoc"  Font-Size="0.85em" 
                            ForeColor="#333333" GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                                    
                         <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                        <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                        <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>
                        <asp:BoundField  DataField="Title" HeaderText="Title"/>
                        <asp:BoundField visible="false"  DataField="LocalId" HeaderText="LocalId"/>
                        <asp:BoundField  DataField="Format" HeaderText="Format"/>
                        <asp:BoundField  DataField="Author" HeaderText="Authors"/>
                        <asp:BoundField  DataField="CreationDate" HeaderText="Creation Date"/>
                        <asp:BoundField  DataField="SizeKb" HeaderText="Size(Kb)"/>
                        <asp:BoundField  DataField="Format" HeaderText="Format"/>
                        </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="selectcompexpLeveldoc" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            OnSelecting="_LevelAllSelecting" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            
                            SelectCommand="SELECT project.title AS PrTitle, specimen.Name AS SpecName, CompExp.Name as CompExpName, document.title, document.LocalId, document.Format, document.Author, document.CreationDate, Round(document.Size/1024,2) as SizeKb, document.role, document.format FROM project,specimen,compexp, document,compexpdocs WHERE (project.idproject = @idproject and project.idproject = specimen.project_idproject AND compexp.specimen_idspecimen = specimen.idspecimen AND compexpdocs.CompExp_idCompExp = compexp.idcompexp and compexpdocs.Document_iddocument=document.iddocument and compexp.type= @type)">
                            <SelectParameters>
             <asp:Parameter Name = "idproject" />
             <asp:Parameter Name = "type" />
             </SelectParameters>
             
                        </asp:SqlDataSource>

                        </div>
                              
                        <h4 id="H4ExpCompPC" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Computer System</a></h4>
                       <div style="float:left; width:100%">
                       <div  runat="server" id="Div20"></div>
                        <asp:GridView ID="GridCompExpLevelPC" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                       Font-Size="0.85em" 
                            DataSourceID="selectcompexpLevelPC" ForeColor="#333333" GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                                      
                              <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                        <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                        <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>
                                  <asp:BoundField  DataField="Software" HeaderText="Software"/>	
                                  <asp:BoundField  DataField="versionNumber" HeaderText="Version Number"/>
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="selectcompexpLevelPC" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title AS PrTitle, specimen.Name AS SpecName, CompExp.Name as CompExpName, Software, versionNumber FROM project, specimen,compexp, computersystem WHERE (project.idproject = @idproject and project.idproject = specimen.project_idproject AND compexp.specimen_idspecimen = specimen.idspecimen AND computersystem.CompExp_idCompExp = compexp.idcompexp and compexp.type= @type)"
                            OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idproject" />
             <asp:Parameter Name = "type" />
             </SelectParameters>
                            </asp:SqlDataSource>
              
                        </div>

                        <h4 id="H4ExpCompVideo" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Experiment Videos</a></h4>
                      <div style="float:left; width:100%">
                      <div  runat="server" id="Div21"></div>
                        <asp:GridView ID="GridCompExpLevelVideo" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                         Font-Size="0.85em" 
                            DataSourceID="selectcompexpLevelVideo" ForeColor="#333333" GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                         		    <Columns>
                         		                            
                         		        <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                                        <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                                        <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>
                                        <asp:BoundField  DataField="Name" HeaderText="Name"/>
                                        <asp:BoundField  DataField="role" HeaderText="Role"/>
                                        <asp:BoundField  DataField="Vsize" HeaderText="Size"/>
                                        <asp:BoundField  DataField="format" HeaderText="Format"/>
                                        <asp:BoundField  DataField="Summary" HeaderText="Summary"/>
			                            <asp:BoundField  DataField="CreationDate" HeaderText="Creation Date"/>			    		    			<asp:BoundField visible="false"  DataField="LocalId" HeaderText="LocalId"/>
                                     </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="selectcompexpLevelVideo" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title AS PrTitle, specimen.Name AS SpecName, CompExp.Name as CompExpName, video.Name, video.CreationDate, video.LocalId, video.role, Round(video.size/1024,2) as Vsize, video.format,video.summary FROM project, specimen, compexp, video WHERE (project.idproject = @idproject and project.idproject = specimen.project_idproject AND compexp.specimen_idspecimen = specimen.idspecimen and video.compexp_idcompexp = compexp.idcompexp and compexp.type= @type)"
                            OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idproject" />
             <asp:Parameter Name = "type" />
             </SelectParameters>
                        </asp:SqlDataSource>
                     
                        </div>
                    </asp:View>  
    			    <asp:View ID="SignalLevelView" runat="server">
                    <h4 id="H4SignalData" runat="server"> <a class="expanded"> &nbsp;  &nbsp; &nbsp;Signal Data</a></h4>
                    <div style="float:left; width:100%">
                   <div  runat="server" id="Div22"></div>
                        <asp:GridView ID="GridSignalLevel" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                            DataSourceID="SelectSignalLevel" ForeColor="#333333" GridLines="None"  Font-Size="0.85em" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                                  
                        <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                        <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                        <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>
                        <asp:BoundField  DataField="SignaLabel" HeaderText="Signal Label"/>
                        <asp:BoundField  DataField="Attribute" HeaderText="Attribute"/>
                        <asp:BoundField  DataField="PhysicalQ" HeaderText="PhysicalQ"/>
                        <asp:BoundField  DataField="Type" HeaderText="Type"/>
                        <asp:BoundField  DataField="Location" HeaderText="Location"/>
                        <asp:BoundField  DataField="Unit" HeaderText="Unit"/>
                         <asp:BoundField  Visible="false" DataField="CompExp_idCompExp" HeaderText="CompExpID"/>
                        <asp:BoundField Visible="false"  DataField="LocalId" HeaderText="LocalId"/>
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectSignalLevel" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title as PrTitle, specimen.Name AS specName,CompExp.Name as CompExpName, SignalResult.CompExp_idCompExp, SignalResult.SignaLabel, SignalResult.Attribute, SignalResult.PhysicalQ, SignalResult.Type, SignalResult.Location, SignalResult.Unit, SignalResult.LocalId FROM project, specimen, compexp, signalresult WHERE (project.idproject= @idproject and project.idproject = specimen.project_idproject and specimen.idspecimen = compexp.specimen_idspecimen and signalresult.compexp_idcompexp = compexp.idcompexp)"
                            OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idproject" />
             </SelectParameters>
                        </asp:SqlDataSource>
                       
                        </div>
                      
                    </asp:View>
                    
                    <asp:View ID="DetailedInfoNoData_View" runat="server">
                
                <div id="Div23" class="rbroundbox">
	<div class="rbtop"><div></div></div>
		<div class="rbcontent"><p>No data available</p>
		</div><!-- /rbcontent -->
	<div class="rbbot"><div></div></div>
</div><!-- /rbroundbox -->
                </asp:View>
                
                 <asp:View ID="CompExpLevelViewNew" runat="server">
                    <h4 id="H4CompExpDataNew" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Experiment/Computation</a></h4>        
                    <div style="float:left; width:100%">
                    <div  runat="server" id="Div28"></div>
                        <asp:GridView ID="GridView1" runat="server" CellPadding="4"  AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"
                            DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None"  OnDataBound="GridView1_DataBound1"
                             Font-Size="0.85em" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                               <Columns>
                                                          
                                    <asp:BoundField  DataField="PrTitle" HeaderText="Project Name"/>
                                    <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                                     <asp:BoundField  DataField="Name" HeaderText="Name"/>
                                    <asp:BoundField  DataField="creationDate" HeaderText="Time Stamp"/>
                                    <asp:BoundField  DataField="ExpComptype" HeaderText="ExpComp Type"/>
                                   
                                    <asp:BoundField  DataField="Repetition" HeaderText="Repetition"/>
                                    
                                    <asp:BoundField  DataField="loadingCoef" HeaderText="Loading Coefficient"/>
                                    <asp:BoundField  DataField="PeakExcitUnit" HeaderText="Peak Excitation Unit"/>
                                    <asp:BoundField  DataField="PeakExcitValue" HeaderText="Peak Excitation Value"/>
                                    <asp:BoundField  DataField="type" HeaderText="type"/>
                                    <asp:BoundField  Visible="false" DataField="Specimen_idSpecimen" HeaderText="SpecimenID"/>
                                    <asp:BoundField Visible="false"  DataField="LocalId" HeaderText="LocalId"/>
                            </Columns>  
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title AS PrTitle, specimen.Name AS SpecName, compexp.Specimen_idSpecimen, compexp.Name, compexp.creationDate, compexp.Repetition, compexp.PeakExcitUnit, compexp.PeakExcitValue, compexp.LocalId, compexp.Type, compexp.loadingCoef, compexp.ExpCompType FROM compexp,specimen,project WHERE ( project.idproject = specimen.project_idproject and specimen.idspecimen = @idspecimen and specimen.idspecimen = compexp.specimen_idspecimen and compexp.type= @type    )"
                             OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idspecimen" />
             <asp:Parameter Name = "type" />
             </SelectParameters>
                        </asp:SqlDataSource>
                        </div>
                        
                        <h4 id="H4ExpCompAgentNew" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Experiment Agents(Personnel)</a></h4>
                        <div style="float:left; width:100%">
                        <div  runat="server" id="Div29"></div>
                        <asp:GridView ID="GridView2" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"  OnDataBound="GridView1_DataBound1"
                         Font-Size="0.85em" 
                            DataSourceID="SqlDataSource2" ForeColor="#333333" GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                                        
                            <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                            <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                            <asp:BoundField  DataField="PersonName" HeaderText="Name"/>
                            <asp:BoundField  DataField="InstitutionAcronym" HeaderText="Institution Acronym"/>
                            <asp:BoundField  DataField="InstitutionName" HeaderText="Institution Name"/>
                           </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title as PrTitle, specimen.Name as SpecName, personnel.Name as PersonName, Institution.Acronym as InstitutionAcronym, Institution.Name as InstitutionName FROM Project INNER JOIN specimen on project.idproject = specimen.project_idproject INNER JOIN compExp ON specimen.idspecimen = compExp.specimen_idspecimen INNER JOIN comp_has_personnel  on comp_has_personnel.compexp_idcompexp = compexp.idcompexp INNER JOIN personnel ON comp_has_personnel.Personnel_idPersonnel = personnel.idPersonnel INNER JOIN Institution on Institution.idinstitution = personnel.institution_idinstitution WHERE (specimen.idspecimen = @idspecimen and compexp.type= @type)"
                            OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idspecimen" />
             <asp:Parameter Name = "type" />
             </SelectParameters>
                        </asp:SqlDataSource>
                        </div>
                       
                           <h4 id="H4ExpCompOlsNew" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Original Loading Signals</a></h4>
                      <div style="float:left; width:100%">
                      <div  runat="server" id="Div30"></div>
                        <asp:GridView ID="GridView3" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                            DataSourceID="SqlDataSource3" ForeColor="#333333" 
                            Font-Size="0.85em" 
                            GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                                         
                        <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                        <asp:BoundField  DataField="specName" HeaderText="Specimen Name"/>
                        <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>
                        <asp:BoundField  DataField="originalLoadingName" HeaderText="Original Loading Name"/>
                        <asp:BoundField  DataField="Nature" HeaderText="Nature"/>
                        <asp:BoundField  DataField="Source" HeaderText="Source"/>
                        <asp:BoundField  DataField="PeakExcitationUnit" HeaderText="Peak Excitation Unit"/>
                         <asp:BoundField  DataField="PeakExcitationValue" HeaderText="Peak Excitation Value"/>
                          
                        <asp:BoundField Visible="false" DataField="LocalId" HeaderText="LocalId"/>
                           </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title AS PrTitle, specimen.name AS specName, compexp.Name AS CompExpName, originalloading.Name AS originalLoadingName, originalloading.Nature, originalloading.Source,  originalloading.PeakExcitationUnit, originalloading.PeakExcitationValue, originalloading.LocalId FROM project INNER JOIN specimen ON specimen.project_idproject = project.idproject INNER JOIN CompExp on specimen.idspecimen = compexp.specimen_idspecimen INNER JOIN compexp_has_dlc on compexp_has_dlc.compexp_idcompexp = compexp.idcompexp INNER JOIN dlc ON dlc.iddlc= compexp_has_dlc.dlc_iddlc INNER JOIN nominalloading_originalloading ON nominalloading_originalloading.NominalLoading = dlc.iddlc INNER JOIN originalloading ON originalloading.idoriginalloading = nominalloading_originalloading.originalLoading  WHERE (specimen.idspecimen = @idspecimen and compexp.type= @type)"
                            OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idspecimen" />
             <asp:Parameter Name = "type" />
             </SelectParameters>
                        </asp:SqlDataSource>
               
                        </div>

                <h4 id="H4ExpCompOlsAttrNew" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp; Original Loading Signals Attributes</a></h4>
                      <div style="float:left; width:100%">
                      <div  runat="server" id="Div31"></div>
                    <asp:GridView ID="GridView4" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"  OnDataBound="GridView1_DataBound1"
                            DataSourceID="SqlDataSource4" ForeColor="#333333" 
                            Font-Size="0.85em" 
                            GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                                         
                                <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                                <asp:BoundField  DataField="specName" HeaderText="Specimen Name"/>
                                <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>
                                <asp:BoundField  DataField="DLCName" HeaderText="DLC Name"/>
                                <asp:BoundField  DataField="OLSName" HeaderText="OLS Name"/>
                                <asp:BoundField  DataField="Sig_Name" HeaderText="Signal Name"/>
                                <asp:BoundField  DataField="Sig_Attr" HeaderText="Signal Attribute"/>
                                <asp:BoundField  DataField="Sig_PQ" HeaderText="Signal Physical Quantity"/>
                                <asp:BoundField  DataField="Sig_Type" HeaderText="Signal Type"/>
                                <asp:BoundField  DataField="Sig_Unit" HeaderText="Signal Unit"/>
                                <asp:BoundField  DataField="Sig_Location" HeaderText="Signal Location"/>
                                
                                <asp:BoundField  Visible="false" DataField="LocalId" HeaderText="LocalId"/>
                           </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title AS PrTitle, specimen.Name AS specName, CompExp.Name AS CompExpName, dlc.Name AS DLCName, originalloading.name as OLSName,inputsignal.Label as Sig_Name,inputsignal.Attribute as  Sig_Attr,inputsignal.PhysicalQ as Sig_PQ,inputsignal.type as Sig_Type,inputsignal.Unit as Sig_Unit,inputsignal.Location as Sig_Location,inputsignal.repetition FROM project, specimen, compexp, dlc, originalloading,compexp_has_dlc, nominalloading_originalloading,inputsignal WHERE (project.idproject = specimen.project_idproject and specimen.idspecimen = compexp.specimen_idspecimen and compexp_has_dlc.CompExp_idCompExp= compexp.idcompexp and compexp_has_dlc.dlc_iddlc = dlc.iddlc and nominalloading_originalloading.NominalLoading = dlc.iddlc and nominalloading_originalloading.originalLoading = OriginalLoading.idOriginalLoading and OriginalLoading.signal_idsignal = inputsignal.idInputSignal and compexp.type= @type and specimen.idspecimen = @idspecimen )"
                            OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idspecimen" />
             <asp:Parameter Name = "type" />
             </SelectParameters>
                        </asp:SqlDataSource>
                
                         </div>
                       
                       
                          <h4 id="H4ExpCompDlcNew" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Detailed Loading Characteristics</a></h4>
                      <div style="float:left; width:100%">
                      <div  runat="server" id="Div32"></div>
                    <asp:GridView ID="GridView5" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"  OnDataBound="GridView1_DataBound1"
                            DataSourceID="SqlDataSource5" ForeColor="#333333" 
                             Font-Size="0.85em" 
                            GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                
                                <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                                <asp:BoundField  DataField="specName" HeaderText="Specimen Name"/>
                                <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>
                                <asp:BoundField  DataField="DLCName" HeaderText="DLC Name"/>
                                <asp:BoundField  DataField="OLSName" HeaderText="OLS Name"/>
                                 <asp:BoundField  DataField="Direction" HeaderText="Direction"/>
                                <asp:BoundField  DataField="loadingCoef" HeaderText="Loading Coefficient(%)"/>
                                <asp:BoundField  DataField="AdditionalParameter" HeaderText="Additional Parameter"/>
                                <asp:BoundField  DataField="notes" HeaderText="Notes"/>
                                <asp:BoundField  Visible="false" DataField="LocalId" HeaderText="LocalId"/>
                           </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT distinct project.title AS PrTitle, specimen.Name AS specName, CompExp.Name AS CompExpName, dlc.Name AS DLCName, dlc.AdditionalParameter,  dlc.loadingCoef, dlc.Notes, dlc.LocalId, originalloading.name as OLSName,nominalloading_originalloading.DirectionHorizon as direction FROM project, specimen, compexp, dlc, originalloading,compexp_has_dlc, nominalloading_originalloading WHERE (project.idproject = specimen.project_idproject and specimen.idspecimen = compexp.specimen_idspecimen and compexp_has_dlc.CompExp_idCompExp= compexp.idcompexp and compexp_has_dlc.dlc_iddlc = dlc.iddlc and nominalloading_originalloading.NominalLoading = dlc.iddlc and nominalloading_originalloading.originalLoading = OriginalLoading.idOriginalLoading and compexp.type= @type and specimen.idspecimen = @idspecimen )"
                            OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idspecimen" />
             <asp:Parameter Name = "type" />
             </SelectParameters>
                        </asp:SqlDataSource>
                
                         </div>
                        
                               <h4 id="H4ExpCompInputFilesNew" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Effective Input Files</a></h4>
                      <div style="float:left; width:100%">
                      <div  runat="server" id="Div33"></div>
                    <asp:GridView ID="GridView6" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"  OnDataBound="GridView1_DataBound1"  
                            DataSourceID="SqlDataSource6" ForeColor="#333333" 
                             Font-Size="0.85em" 
                            GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                         
                                <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                                <asp:BoundField  DataField="specName" HeaderText="Specimen Name"/>
                                <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>
                                <asp:BoundField  DataField="DLCName" HeaderText="DLC Name"/>
                                <asp:BoundField  DataField="OLSName" HeaderText="OLS Name"/>
                                <asp:BoundField  DataField="EffectiveInputFile" HeaderText="Effective Input File name"/>
                                <asp:BoundField  DataField="InputSig_Attr" HeaderText="Effective Input File Attribute"/>
                                <asp:BoundField  DataField="InputSig_PQ" HeaderText="Effective Input File Physical Quantity"/>
                                <asp:BoundField  DataField="InputSig_Type" HeaderText="Effective Input File Type"/>
                                <asp:BoundField  DataField="InputSig_Unit" HeaderText="Effective Input File Unit"/>
                                <asp:BoundField  DataField="InputSig_Location" HeaderText="Effective Input File Location"/>
                                
                                 
                        
                                <asp:BoundField  Visible="false" DataField="LocalId" HeaderText="LocalId"/>
                           </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource6" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT  project.title AS PrTitle, specimen.Name AS specName, CompExp.Name AS CompExpName, dlc.Name AS DLCName, originalloading.name as OLSName,inputsignal.Label as EffectiveInputFile,inputsignal.Attribute as InputSig_Attr,inputsignal.PhysicalQ as InputSig_PQ,inputsignal.type as InputSig_Type,inputsignal.Unit as InputSig_Unit,inputsignal.Location as InputSig_Location,inputsignal.repetition FROM project, specimen, compexp, dlc, originalloading,compexp_has_dlc, nominalloading_originalloading,inputsignal WHERE (project.idproject = specimen.project_idproject and specimen.idspecimen = compexp.specimen_idspecimen and compexp_has_dlc.CompExp_idCompExp= compexp.idcompexp and compexp_has_dlc.dlc_iddlc = dlc.iddlc and nominalloading_originalloading.NominalLoading = dlc.iddlc and nominalloading_originalloading.originalLoading = OriginalLoading.idOriginalLoading and nominalloading_originalloading.EffectiveInputFile = inputsignal.idInputSignal and compexp.type= @type and specimen.idspecimen = @idspecimen  ) "
                            OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idspecimen" />
             <asp:Parameter Name = "type" />
 
             </SelectParameters>
                        </asp:SqlDataSource>
                
                         </div>
                        
                        
                     
                        <h4 id="H4ExpCompMeshModelNew" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Mesh Models</a></h4>
                        <div style="float:left; width:100%"> <div  runat="server" id="Div34"></div>
                        <asp:GridView ID="GridView7" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                            DataSourceID="SqlDataSource7" ForeColor="#333333" 
                             Font-Size="0.85em" 
                            GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                                        
                            <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                        <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                        <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>
                        <asp:BoundField  DataField="Nonlinearity" HeaderText="Nonlinearity"/>
                        <asp:BoundField  DataField="SymmetryType" HeaderText="SymmetryType"/>
                        <asp:BoundField  DataField="material" HeaderText="Material"/>
                        <asp:BoundField  Visible="false" DataField="LocalId" HeaderText="LocalId"/>
                           </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource7" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title AS PrTitle,specimen.name AS SpecName, compexp.name as CompExpName ,meshmodel.Nonlinearity ,meshmodel.SymmetryType, meshmodel.LocalId, material.name as material from project,specimen,compexp, meshmodel,material,meshmodel_has_material WHERE (specimen.idspecimen = @idspecimen and project.idproject =  project.idproject and compexp.specimen_idspecimen = specimen.idspecimen and meshmodel.CompExp_idcompexp = compexp.idcompexp and compexp.type= @type and meshmodel.idmeshmodel = meshmodel_has_material.meshmodel_idmeshmodel and meshmodel_has_material.material_idmaterial = material.idmaterial)"
                            OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idspecimen" />
             <asp:Parameter Name = "type" />
             </SelectParameters>
                        </asp:SqlDataSource>
                  
                        </div>
                       
                        <h4 id="h4expCompMeshModelDocNew" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Mesh Models Documents</a></h4>
                        <div style="float:left; width:100%">
                        <div  runat="server" id="Div35"></div>
                        <asp:GridView ID="GridView8" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                            DataSourceID="SqlDataSource8" ForeColor="#333333" 
                            Font-Size="0.85em" 
                            GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                                          
                          <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                        <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                        <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>    
                        <asp:BoundField  DataField="Title" HeaderText="Title"/>
                        <asp:BoundField  DataField="Author" HeaderText="Author"/>
                        <asp:BoundField  DataField="SizeKb" HeaderText="Size(Kb)"/>
                        <asp:BoundField  DataField="Format" HeaderText="Format"/>
                        <asp:BoundField  DataField="Abstract" HeaderText="Abstract"/>
                        <asp:BoundField  DataField="role" HeaderText="Role"/>
                        <asp:BoundField  DataField="scope" HeaderText="Scope"/>
                        <asp:BoundField  DataField="creationdate" HeaderText="Creation Date"/>
                        <asp:BoundField Visible="false"  DataField="LocalId " HeaderText="LocalId "/>
                           </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource8" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title AS PrTitle, specimen.Name AS SpecName,CompExp.Name as CompExpName, meshmodeldocuments.Title , meshmodeldocuments.Author, Round(meshmodeldocuments.Size/1024,2) as SizeKb ,meshmodeldocuments.Format, meshmodeldocuments.Abstract,meshmodeldocuments.scope, meshmodeldocuments.role, meshmodeldocuments.creationdate, meshmodel.LocalId from project, specimen,compexp, meshmodel,meshmodeldocuments WHERE (specimen.idspecimen = @idspecimen and project.idproject = specimen.project_idproject AND compexp.specimen_idspecimen = specimen.idspecimen AND meshmodel.CompExp_idcompexp = compexp.idcompexp AND  meshmodeldocuments.MeshModel_IdMeshModel = meshmodel.idMeshModel and compexp.type= @type)"
                            OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idspecimen" />
             <asp:Parameter Name = "type" />
             </SelectParameters>
                        </asp:SqlDataSource>
                       
                        </div>

                        <h4  id="H4ExpCompMeshModelImgNew" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Mesh Models Images</a></h4>
                        <div style="float:left; width:100%">
                        <div  runat="server" id="Div36"></div>
                        <asp:GridView ID="GridView9" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                            DataSourceID="SqlDataSource9" ForeColor="#333333" 
                             Font-Size="0.85em" 
                            GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                                         
                              <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                        <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                        <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>
                        <asp:BoundField  DataField="Name" HeaderText="Image Name"/>
                        <asp:BoundField  DataField="PictureDate" HeaderText="Picture Date"/>
                        <asp:BoundField  DataField="role" HeaderText="Role"/>
                        <asp:BoundField  DataField="author" HeaderText="Author"/>
                        <asp:BoundField  DataField="Imgsize" HeaderText="Size"/>
                        <asp:BoundField  DataField="summary" HeaderText="Summary"/>
                        <asp:BoundField  DataField="format" HeaderText="Format"/>
                         <asp:BoundField Visible="false" DataField="ImageLocalID" HeaderText="ImageLocalID"/>
                        <asp:BoundField Visible="false" DataField="MeshModelLocalID" HeaderText="MeshModelLocalID"/>
                           </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource9" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title AS PrTitle, specimen.Name AS SpecName, CompExp.Name as CompExpName, meshmodelpicture.Name , meshmodelpicture.PictureDate, meshmodelpicture.role, meshmodelpicture.author ,meshmodelpicture.format, Round(meshmodelpicture.size/1024,3) as imgSize, meshmodelpicture.summary, meshmodelpicture.LocalID as ImageLocalID , meshmodel.LocalId as MeshModelLocalID  from project, specimen, compexp, meshmodel,meshmodelpicture WHERE (specimen.idspecimen = @idspecimen and project.idproject = specimen.project_idproject AND compexp.specimen_idspecimen = specimen.idspecimen AND meshmodel.CompExp_idcompexp = compexp.idcompexp  AND MeshModelPicture.MeshModel_IdMeshModel = meshmodel.idMeshModel AND meshmodel.CompExp_idcompexp = compexp.idcompexp and compexp.type= @type)"
                            OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idspecimen" />
             <asp:Parameter Name = "type" />
             </SelectParameters>
                        </asp:SqlDataSource>
            
                        </div>
                      
                        <h4 id="H4ExpCompImgNew" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Experiment Images</a></h4>
                       <div style="float:left; width:100%">             
                        <div  runat="server" id="Div37"></div>
                        <asp:GridView ID="GridView10" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                            DataSourceID="SqlDataSource10"  Font-Size="0.85em" 
                            ForeColor="#333333" GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                                    
                             <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                        <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                        <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>
                        <asp:BoundField  DataField="title" HeaderText="Title"/>
                        <asp:BoundField  DataField="creationDate" HeaderText="Creation Date"/>
                        <asp:BoundField  DataField="role" HeaderText="Role"/>
                        <asp:BoundField  DataField="author" HeaderText="Author"/>
                        <asp:BoundField  DataField="summary" HeaderText="Summary"/>
                        <asp:BoundField  DataField="format" HeaderText="Format"/>
                        <asp:BoundField  DataField="Imgsize" HeaderText="Size"/>
                        <asp:BoundField Visible="false"  DataField="LocalId" HeaderText="LocalId"/>
                       
                           </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource10" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            OnSelecting="_LevelAllSelecting" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title AS PrTitle, specimen.Name AS SpecName, CompExp.Name as CompExpName, compexppictures.title, compexppictures.creationDate,compexppictures.LocalId, compexppictures.role, round(compexppictures.size/1024,2) as ImgSize ,compexppictures.summary,compexppictures.format FROM project, specimen, compexp, compexppictures WHERE (specimen.idspecimen = @idspecimen and project.idproject =  specimen.project_idproject AND compexp.specimen_idspecimen = specimen.idspecimen  AND compexppictures.CompExp_idCompExp = compexp.idcompexp and compexp.type= @type)">
                            <SelectParameters>
                                <asp:Parameter Name="idspecimen" />
                                <asp:Parameter Name = "type" />
                            </SelectParameters>
                        </asp:SqlDataSource>
              
                        </div>

                        <h4 id="H4ExpCompDocNew" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Computations/experiment Documents</a></h4>
                        <div style="float:left; width:100%">
                        <div  runat="server" id="Div38"></div>
                        <asp:GridView ID="GridView11" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                            DataSourceID="SqlDataSource11"  Font-Size="0.85em" 
                            ForeColor="#333333" GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                                    
                         <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                        <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                        <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>
                        <asp:BoundField  DataField="Name" HeaderText="Name"/>
                        <asp:BoundField visible="false"  DataField="LocalId" HeaderText="LocalId"/>
                        <asp:BoundField  DataField="Format" HeaderText="Format"/>
                        <asp:BoundField  DataField="Author" HeaderText="Authors"/>
                        <asp:BoundField  DataField="CreationDate" HeaderText="Creation Date"/>
                        <asp:BoundField  DataField="SizeKb" HeaderText="Size(Kb)"/>
                        <asp:BoundField  DataField="Type" HeaderText="Type"/>
                        </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource11" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            OnSelecting="_LevelAllSelecting" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            
                            SelectCommand="SELECT project.title AS PrTitle, specimen.Name AS SpecName, CompExp.Name as CompExpName, document.title, document.LocalId, document.Format, document.Author, document.CreationDate, Round(document.Size/1024,2) as SizeKb, document.role, document.format FROM project,specimen,compexp, document,compexpdocs WHERE (specimen.idspecimen = @idspecimen and  project.idproject = specimen.project_idproject AND compexp.specimen_idspecimen = specimen.idspecimen AND compexpdocs.CompExp_idCompExp = compexp.idcompexp and compexpdocs.Document_iddocument=document.iddocument and compexp.type= @type)">
                            <SelectParameters>
             <asp:Parameter Name = "idspecimen" />
             <asp:Parameter Name = "type" />
             </SelectParameters>
             
                        </asp:SqlDataSource>

                        </div>
                              
                        <h4 id="H4ExpCompPcNew" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Computer System</a></h4>
                       <div style="float:left; width:100%">
                       <div  runat="server" id="Div39"></div>
                        <asp:GridView ID="GridView12" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                       Font-Size="0.85em" 
                            DataSourceID="SqlDataSource12" ForeColor="#333333" GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                                      
                              <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                        <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                        <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>
                                  <asp:BoundField  DataField="Software" HeaderText="Software"/>	
                                  <asp:BoundField  DataField="versionNumber" HeaderText="Version Number"/>
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource12" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title AS PrTitle, specimen.Name AS SpecName, CompExp.Name as CompExpName, Software, versionNumber FROM project, specimen,compexp, computersystem WHERE (specimen.idspecimen = @idspecimen and project.idproject = specimen.project_idproject AND compexp.specimen_idspecimen = specimen.idspecimen AND computersystem.CompExp_idCompExp = compexp.idcompexp and compexp.type= @type)"
                            OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idspecimen" />
             <asp:Parameter Name = "type" />
             </SelectParameters>
                            </asp:SqlDataSource>
              
                        </div>

                        <h4 id="H4ExpCompVIdeosNew" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp;Experiment Videos</a></h4>
                      <div style="float:left; width:100%">
                      <div  runat="server" id="Div40"></div>
                        <asp:GridView ID="GridView13" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                         Font-Size="0.85em" 
                            DataSourceID="SqlDataSource13" ForeColor="#333333" GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                         		    <Columns>
                         		                            
                         		        <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                                        <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                                        <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>
                                        <asp:BoundField  DataField="Name" HeaderText="Name"/>
                                        <asp:BoundField  DataField="role" HeaderText="Role"/>
                                        <asp:BoundField  DataField="Vsize" HeaderText="Size"/>
                                        <asp:BoundField  DataField="format" HeaderText="Format"/>
                                        <asp:BoundField  DataField="Summary" HeaderText="Summary"/>
			                            <asp:BoundField  DataField="CreationDate" HeaderText="Creation Date"/>			    		    			<asp:BoundField visible="false"  DataField="LocalId" HeaderText="LocalId"/>
                                     </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource13" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title AS PrTitle, specimen.Name AS SpecName, CompExp.Name as CompExpName, video.Name, video.CreationDate, video.LocalId, video.role, Round(video.size/1024,2) as Vsize, video.format,video.summary FROM project, specimen, compexp, video WHERE (specimen.idspecimen = @idspecimen and project.idproject = specimen.project_idproject AND compexp.specimen_idspecimen = specimen.idspecimen and video.compexp_idcompexp = compexp.idcompexp and compexp.type= @type)"
                            OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idspecimen" />
             <asp:Parameter Name = "type" />
             </SelectParameters>
                        </asp:SqlDataSource>
                     
                        </div>
                    </asp:View>  
                
                
                  <asp:View ID="SignalLevelViewNew" runat="server">
                    <h4 id="H4SignalDataNew" runat="server"> <a class="expanded"> &nbsp;  &nbsp; &nbsp;Signal Data</a></h4>
                    <div style="float:left; width:100%">
                   <div  runat="server" id="Div41"></div>
                        <asp:GridView ID="GridView14" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowDataBound="ExpLevel_RowDataBound"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                            DataSourceID="SqlDataSource14" ForeColor="#333333" GridLines="None"  Font-Size="0.85em" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                                  
                        <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                        <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
                        <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>
                        <asp:BoundField  DataField="SignaLabel" HeaderText="Signal Label"/>
                        <asp:BoundField  DataField="Attribute" HeaderText="Attribute"/>
                        <asp:BoundField  DataField="PhysicalQ" HeaderText="PhysicalQ"/>
                        <asp:BoundField  DataField="Type" HeaderText="Type"/>
                        <asp:BoundField  DataField="Location" HeaderText="Location"/>
                        <asp:BoundField  DataField="Unit" HeaderText="Unit"/>
                         <asp:BoundField  Visible="false" DataField="CompExp_idCompExp" HeaderText="CompExpID"/>
                        <asp:BoundField Visible="false"  DataField="LocalId" HeaderText="LocalId"/>
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource14" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title as PrTitle, specimen.Name AS specName,CompExp.Name as CompExpName, SignalResult.CompExp_idCompExp, SignalResult.SignaLabel, SignalResult.Attribute, SignalResult.PhysicalQ, SignalResult.Type, SignalResult.Location, SignalResult.Unit, SignalResult.LocalId FROM project, specimen, compexp, signalresult WHERE (project.idproject = specimen.project_idproject and specimen.idspecimen = compexp.specimen_idspecimen and signalresult.compexp_idcompexp = compexp.idcompexp and compexp.idcompexp = @idexperiment)"
                            OnSelecting="_LevelAllSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idexperiment" />
             </SelectParameters>
                        </asp:SqlDataSource>
                       
                        </div>
                      
                    </asp:View>
                    
                
                
                </asp:MultiView>
      
        </div>    
        <div id="Download" class="tab_content">
           <!--Content-->
          <asp:MultiView ID="DownloadMultiView" runat="server" ActiveViewIndex="1">
    				            
    				        
                  
                    <asp:View ID="DownloadPr_View" runat="server">
                
                  <h4 id="H3PrDocDown" runat="server"></h4>
            <div style="float:left; width:100%">
            <div id="PrDocDowndiv" runat="server"></div>
            <asp:GridView ID="DownloadPrDoc_GrdV" runat="server" CellPadding="4" DataKeyNames="LocalID" visible = "false"
            CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnRowCommand="OnRowCommand" OnDataBound="GridView1_DataBound1"
                DataSourceID="SelectProjDocsDown" ForeColor="#333333" GridLines="None"  
               Font-Size="0.85em" HorizontalAlign="Center" AutoGenerateColumns="False">
                <RowStyle BackColor="#EFF3FB" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                 <Columns>
                        <asp:BoundField  DataField="Title" HeaderText="Title"/>
                        <asp:BoundField  DataField="Author" HeaderText="Author"/>
                        <asp:BoundField  DataField="DocDate" HeaderText="Document Date"/>
                        <asp:BoundField  DataField="role" HeaderText="Role"/>
                        <asp:BoundField  DataField="abstract" HeaderText="Abstract"/>
                        <asp:BoundField  DataField="scope" HeaderText="Scope"/>
                        <asp:BoundField  Visible="true" DataField="dlinfo" HeaderText="Download Info"/>
                        <asp:BoundField  Visible="false" DataField="LocalID" HeaderText="LocalId"/>
                        <asp:TemplateField ItemStyle-Width="40px" HeaderText="Download">
                        <ItemTemplate>
                       <asp:ImageButton Width="30px" Height="30px" ID="ImgBtn"   runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  ImageUrl="~/images/download blue.png"/>      
                        </ItemTemplate>
                        </asp:TemplateField>
                       
                        </Columns>
            </asp:GridView> 
                 <asp:SqlDataSource ID="SelectProjDocsDown" runat="server" 
                ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                SelectCommand="SELECT idProjectReport, Title, Author, Abstract, LocalId as LocalID, Concat_ws(',',concat_ws(' ',Round(Size/1024,2),'KB') ,format) as dlinfo,role,abstract,scope,docdate FROM projectreport WHERE (Project_idProject = @idproject)"
                 OnSelecting= "_InitialProjectSelecting">
                 <SelectParameters>
                                <asp:Parameter Name="idproject" />
                            </SelectParameters>
            </asp:SqlDataSource>
       
            
            </div>
                 
                  <h4 id="H3SpecDocDown" runat="server"><a class="expanded"></a></h4>
           <div style="float:left; width:100%">
           <div id="SpecDocDowndiv" runat="server"></div>
            <asp:GridView ID="DownloadSpecDoc_GrdV" runat="server" CellPadding="4" DataKeyNames="LocalID" visible = "false" OnRowCommand="OnRowCommand"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                            DataSourceID="SelectSpecDocByPrID" ForeColor="#333333" GridLines="None"
                            Font-Size="0.85em" HorizontalAlign="Center" AutoGenerateColumns="False">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                           
                        <asp:BoundField Visible="false"  DataField="idSpecimenReport" HeaderText="idSpecimenReport"/>
                        <asp:BoundField  DataField="Title" HeaderText="Title"/>
                        <asp:BoundField  DataField="Author" HeaderText="Author"/>
                        <asp:BoundField  DataField="Abstract" HeaderText="Abstract"/>
                        <asp:BoundField  DataField="Type" HeaderText="Type"/>
                         <asp:BoundField  DataField="role" HeaderText="Role"/>
                       <asp:BoundField  Visible="true" DataField="dlinfo" HeaderText="Download Info"/>
                        <asp:BoundField  Visible="false" DataField="LocalID" HeaderText="LocalId"/>
                        <asp:TemplateField ItemStyle-Width="40px" HeaderText="Download">
                        <ItemTemplate>
                       <asp:ImageButton Width="30px" Height="30px" ID="ImgBtn"   runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  ImageUrl="~/images/download blue.png"/>      
                        </ItemTemplate>
                        </asp:TemplateField>
                        
                        </Columns>
                        </asp:GridView>
           <asp:SqlDataSource ID="SelectSpecDocByPrID" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT distinct idSpecimenReport, specimenreport.Title, Author, Abstract,specimenreport.LocalId as LocalID,specimenreport.role,Concat_ws(',',concat_ws(' ',Round(Size/1024,2),'KB') ,format) as dlinfo,specimenreport.scope as type FROM specimenreport,specimen,project WHERE (project.idproject = specimen.project_idproject and specimenreport.Specimen_idSpecimen = specimen.idspecimen and project.idproject = @idproject)"
                            OnSelecting = "_InitialProjectSelecting">
                       <SelectParameters>
            <asp:Parameter Name = "idproject" />
            </SelectParameters>
                        </asp:SqlDataSource>
            </div>
                         
             <h4 id="H3SpecImgDown" runat="server"><a class="expanded"> </a></h4>
             <div style="float:left; width:100%">
             <div id="SpecImgDowndiv" runat="server"></div>
             <asp:GridView ID="DownloadSpecImg_GrdV" runat="server" CellPadding="4" DataKeyNames="LocalID" visible = "false"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnRowCommand="OnRowCommand" OnDataBound="GridView1_DataBound1"
                            DataSourceID="SelectSpecPicByPrID" ForeColor="#333333" GridLines="None"
                            Font-Size="0.85em" HorizontalAlign="Center" AutoGenerateColumns="False">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                             <Columns>
                             
                       	        <asp:BoundField  DataField="projecttitle" HeaderText="Project Title"/>
                         	            <asp:BoundField  DataField="SpecName" HeaderText="Specimen Name"/>
			                            <asp:BoundField  DataField="Name" HeaderText="Name"/>	
			                            <asp:BoundField  DataField="CreationDate" HeaderText="Creation Date"/>			    		
                                   	    <asp:BoundField  DataField="role" HeaderText="Role"/>			    		
                                	    <asp:BoundField DataField="Author" HeaderText="Author"/>
                                <asp:BoundField  Visible="true" DataField="dlinfo" HeaderText="Download Info"/>
                        <asp:BoundField  Visible="false" DataField="LocalID" HeaderText="LocalId"/>
                        <asp:TemplateField ItemStyle-Width="40px" HeaderText="Download">
                        <ItemTemplate>
                       <asp:ImageButton Width="30px" Height="30px" ID="ImgBtn"   runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  ImageUrl="~/images/download blue.png"/>      
                        </ItemTemplate>
                        </asp:TemplateField>
                        
                          
                        </Columns>
                        </asp:GridView>
             <asp:SqlDataSource ID="SelectSpecPicByPrID" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT distinct specimenpictures.Name as Name, specimenpictures.Creationdate, specimenpictures.role,specimenPictures.LocalId AS LocalID, specimenpictures.author,Concat_ws(',',concat_ws(' ',Round(specimenpictures.size/1024,2),'KB') ,specimenpictures.format) as dlinfo, project.title as projecttitle, specimen.name as SpecName FROM project,specimen,specimenpictures WHERE (specimenpictures.Specimen_idSpecimen = specimen.idspecimen and project.idproject = specimen.project_idproject and project.idproject = @idproject)"
                            OnSelecting = "_InitialProjectSelecting">
                            <SelectParameters>
            <asp:Parameter Name = "idproject" />
            </SelectParameters>
                        </asp:SqlDataSource>
             </div>
                       

                    <h4 id="H3MeshModelDocDown" runat="server"><a class="expanded"></a></h4>
                        <div style="float:left; width:100%">
                        <div id="MeshModelDocDowndiv" runat="server"></div>
                        <asp:GridView ID="DownloadMeshModelDoc_GrdV" runat="server" CellPadding="4" DataKeyNames="LocalID" visible = "false"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnRowCommand="OnRowCommand" OnDataBound="GridView1_DataBound1"
                            DataSourceID="MeshModelsDocsByPrID" ForeColor="#333333"   AutoGenerateColumns="false"
                            Font-Size="0.85em"
                            GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                             <Columns>
                             
                        <asp:BoundField  DataField="Title" HeaderText="Title"/>
                        <asp:BoundField  DataField="Author" HeaderText="Author"/>
                   <asp:BoundField  DataField="role" HeaderText="Role"/>
                   <asp:BoundField  DataField="abstract" HeaderText="Abstract"/>
                        <asp:BoundField  DataField="scope" HeaderText="Scope"/>
                        <asp:BoundField  DataField="creationdate" HeaderText="Creation Date"/>
                       <asp:BoundField  Visible="true" DataField="dlinfo" HeaderText="Download Info"/>
                        <asp:BoundField  Visible="false" DataField="LocalID" HeaderText="LocalId"/>
                        <asp:TemplateField ItemStyle-Width="40px" HeaderText="Download">
                        <ItemTemplate>
                       <asp:ImageButton Width="30px" Height="30px" ID="ImgBtn"   runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  ImageUrl="~/images/download blue.png"/>      
                        </ItemTemplate>
                        </asp:TemplateField>
                        
                        
                         </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="MeshModelsDocsByPrID" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT distinct meshmodeldocuments.Title , meshmodeldocuments.Author, meshmodeldocuments.role, meshmodeldocuments.abstract, meshmodeldocuments.scope, meshmodeldocuments.creationdate,  Concat_ws(',',concat_ws(' ',Round(meshmodeldocuments.size/1024,2),'KB') ,meshmodeldocuments.format) as dlinfo ,meshmodeldocuments.Abstract,meshmodeldocuments.LocalId as LocalID from meshmodeldocuments,meshmodel,compexp,specimen,project WHERE (meshmodeldocuments.MeshModel_IdMeshModel = meshmodel.idMeshModel AND meshmodel.CompExp_idcompexp = compexp.idcompexp and specimen.idspecimen = compexp.specimen_idspecimen and specimen.project_idproject = project.idproject and project.idproject = @idproject)"
                            OnSelecting="_InitialProjectSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idproject" />
             </SelectParameters>
                        </asp:SqlDataSource>
                       </div>
                     
                              
                         <h4 id="H3MeshModelImgDown" runat="server"><a class="expanded"></a></h4>
                        <div style="float:left; width:100%">
                        <div id="MeshModelImgDowndiv" runat="server"></div>
                        <asp:GridView ID="DownloadMeshModelImg_GrdV" runat="server" CellPadding="4" DataKeyNames="LocalID" visible = "false"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnRowCommand="OnRowCommand" OnDataBound="GridView1_DataBound1"
                            DataSourceID="MeshModelsPicsDown" ForeColor="#333333" AutoGenerateColumns="false"
                             Font-Size="0.85em"
                            GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                             <Columns>
                             <asp:BoundField  DataField="Name" HeaderText="Name"/>
                        <asp:BoundField  DataField="PictureDate" HeaderText="Picture Date"/>
                        <asp:BoundField  Visible="false" DataField="LocalID" HeaderText="LocalId"/>
                        <asp:TemplateField ItemStyle-Width="40px" HeaderText="Download">
                        <ItemTemplate>
                       <asp:ImageButton Width="30px" Height="30px" ID="ImgBtn"   runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  ImageUrl="~/images/download blue.png"/>      
                        </ItemTemplate>
                        </asp:TemplateField>
                        
                        
                         </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="MeshModelsPicsDown" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT distinct meshmodelpicture.Name , meshmodelpicture.PictureDate, meshmodelpicture.LocalID as meshmodelImgID ,meshmodel.LocalId as LocalID from meshmodel,meshmodelpicture,compexp,specimen,project WHERE (MeshModelPicture.MeshModel_IdMeshModel = meshmodel.idMeshModel AND meshmodel.CompExp_idcompexp = compexp.idcompexp AND compexp.specimen_idspecimen=specimen.idspecimen AND specimen.project_idproject= project.idproject and project.idproject=@idproject)"
                            OnSelecting="_InitialProjectSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idproject" />
             </SelectParameters>
                        </asp:SqlDataSource>
                        </div>
                    
                        <h4 id="H3ExpImgDown" runat="server"><a class="expanded"> </a></h4>
                        <div style="float:left; width:100%">
                        <div id="ExpImgDowndiv" runat="server"></div>
                        <asp:GridView ID="DownloadExpImg_GrdV" runat="server" CellPadding="4" DataKeyNames="LocalID" visible = "false"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnRowCommand="OnRowCommand" OnDataBound="GridView1_DataBound1"
                            DataSourceID="SelectExpPicDown" Font-Size="0.85em"  AutoGenerateColumns="false"
                            ForeColor="#333333" GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                              <Columns>
                              
                        <asp:BoundField  DataField="title" HeaderText="Title"/>
                        <asp:BoundField  DataField="CreationDate" HeaderText="Creation Date"/>
                        <asp:BoundField  Visible="true" DataField="dlinfo" HeaderText="Download Info"/>
                        <asp:BoundField  Visible="false" DataField="LocalID" HeaderText="LocalId"/>
                        <asp:TemplateField ItemStyle-Width="40px" HeaderText="Download">
                        <ItemTemplate>
                       <asp:ImageButton Width="30px" Height="30px" ID="ImgBtn"   runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  ImageUrl="~/images/download blue.png"/>      
                        </ItemTemplate>
                        </asp:TemplateField>
                        </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectExpPicDown" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            OnSelecting="_InitialProjectSelecting" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT distinct compexppictures.title, compexppictures.CreationDate,compexppictures.LocalId as LocalID,  Concat_ws(',',concat_ws(' ',Round(compexppictures.size/1024,2),'KB') ,compexppictures.format) as dlinfo FROM compexppictures,compexp,specimen,project WHERE (compexppictures.CompExp_idCompExp = compexp.idcompexp and compexp.specimen_idspecimen = specimen.idspecimen and specimen.project_idproject = project.idproject and project.idproject=@idproject)">
                            <SelectParameters>
                                <asp:Parameter Name="idproject" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        </div>
                        
                         <h4 id="H3ExpDocDown" runat="server"><a class="expanded"> </a></h4>
                         <div style="float:left; width:100%">
                         <div id="ExpDocDowndiv" runat="server"></div>
                        <asp:GridView ID="DownloadExpDoc_GrdV" runat="server" CellPadding="4" DataKeyNames="LocalID" visible = "false" OnRowCommand="OnRowCommand" 
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" OnDataBound="GridView1_DataBound1"
                            DataSourceID="SelectExpDocDown" Font-Size="0.85em" AutoGenerateColumns="false"
                            ForeColor="#333333" GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                            
                        <asp:BoundField  DataField="title" HeaderText="title"/>
                     
                        <asp:BoundField  DataField="Abstract" HeaderText="Abstract"/>
                        <asp:BoundField  DataField="Author" HeaderText="Authors"/>
                        <asp:BoundField  DataField="role" HeaderText="Role"/> 
                        <asp:BoundField  DataField="scope" HeaderText="Scope"/>                       
                        <asp:BoundField  DataField="CreationDate" HeaderText="Creation Date"/>
                        <asp:BoundField  Visible="true" DataField="dlinfo" HeaderText="Download Info"/>
                        <asp:BoundField  Visible="false" DataField="LocalID" HeaderText="LocalId"/>
                        <asp:TemplateField ItemStyle-Width="40px" HeaderText="Download">
                        <ItemTemplate>
                       <asp:ImageButton Width="30px" Height="30px" ID="ImgBtn"   runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  ImageUrl="~/images/download blue.png"/>      
                        </ItemTemplate>
                        </asp:TemplateField>
                          
                        </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectExpDocDown" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            OnSelecting="_InitialProjectSelecting" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>"     
                            SelectCommand="SELECT distinct document.title,document.Abstract, document.LocalId as LocalID, document.Author, document.CreationDate, Concat_ws(',',concat_ws(' ',Round(document.size/1024,2),'KB') ,document.format) as dlinfo, document.role, document.scope FROM document,compexp,specimen,project,compexpdocs WHERE (document.iddocument = compexpdocs.document_iddocument and compexpdocs.CompExp_idCompExp = compexp.idcompexp and compexp.specimen_idspecimen = specimen.idspecimen and project.idproject=specimen.project_idproject and project.idproject=@idproject)">
                            <SelectParameters>
             <asp:Parameter Name = "idproject" />
             </SelectParameters>
             
                        </asp:SqlDataSource>

                        </div>
                     
                       
                            <h4 id="H3ExpVidDown" runat="server"><a class="expanded"> </a></h4>   
                        <div style="float:left; width:100%">
                        <div id="ExpVidDowndiv" runat="server"></div>
                        <asp:GridView ID="DownloadExpVid_GrdV" runat="server" CellPadding="4" Visible="false" AutoGenerateColumns="false" 
                        Font-Size="0.85em" OnDataBound="GridView1_DataBound1" OnRowCommand="OnRowCommand"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" DataKeyNames="LocalID"
                            DataSourceID="SelectExpVidDown" ForeColor="#333333" GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                            
                        <asp:BoundField  DataField="Name" HeaderText="Name"/>
                        <asp:BoundField  DataField="CreationDate" HeaderText="Creation Date"/>
                        <asp:BoundField Visible="false"  DataField="LocalID" HeaderText="LocalId"/>
                        
                        <asp:TemplateField ItemStyle-Width="30px" HeaderText="Download">
                        <ItemTemplate>
                        <asp:ImageButton ID="ImgBtn" runat="server" Width="30px" Height="30px" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ImageUrl="~/images/download blue.png"/>
                        </ItemTemplate>
                        </asp:TemplateField>
                        
                        
                        </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectExpVidDown" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT distinct Video.Name,Video.CreationDate , Video.LocalId as LocalID FROM video,project,compexp,specimen WHERE (video.compexp_idcompexp = compexp.idcompexp and compexp.specimen_idspecimen = specimen.idspecimen and project.idproject = specimen.project_idproject and project.idproject = @idproject)"
                            OnSelecting="_InitialProjectSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idproject" />
             </SelectParameters>
                        </asp:SqlDataSource>
                        </div>

                    
                         <h4 id="H3SignalDown" runat="server"><a class="expanded"></a></h4>
                         <div style="float:left; width:100%">
                         <div id="SignaldDowndiv" runat="server"></div>
                         <asp:GridView ID="DownloadSignalResult_GrdV" runat="server" CellPadding="4"  AutoGenerateColumns="false" visible = "false"  OnDataBound="GridView1_DataBound1"
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" DataKeyNames="LocalID" OnRowCommand="OnRowCommand"
                            DataSourceID="SelectSignalResultDown" ForeColor="#333333" GridLines="None" 
                            Font-Size="0.85em" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                            
                           
                        <asp:BoundField  DataField="SignaLabel" HeaderText="SignaLabel"/>     
                        <asp:BoundField  DataField="Attribute" HeaderText="Attribute"/>
                        <asp:BoundField  DataField="PhysicalQ" HeaderText="PhysicalQ"/>
                        <asp:BoundField  DataField="Type" HeaderText="Type"/>
                        <asp:BoundField  DataField="Location" HeaderText="Location"/>
                        <asp:BoundField  DataField="Unit" HeaderText="Unit"/>
                        <asp:BoundField Visible="false"  DataField="LocalID" HeaderText="LocalId"/>
                        <asp:TemplateField ItemStyle-Width="30px" HeaderText="Download">
                        <ItemTemplate>
                        <asp:ImageButton ID="ImgBtn"   runat="server" Width="30px" Height="30px" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  ImageUrl="~/images/download blue.png"/>
                        </ItemTemplate>
                        </asp:TemplateField>
                        
                        
                        </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectSignalResultDown" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT SignaLabel, Attribute, PhysicalQ, SignalResult.Type, SignalResult.Location, SignalResult.Unit, SignalResult.LocalId as LocalID FROM signalresult,compexp,specimen,project WHERE (project.idproject=specimen.project_idproject and specimen.idspecimen=compexp.specimen_idspecimen and compexp.idcompexp=signalresult.compexp_idcompexp and project.idproject=@idproject)"
                            OnSelecting="_InitialProjectSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idproject" />
             </SelectParameters>
                        </asp:SqlDataSource>
                        </div>
                        
                        
                <h4 id="H3OLS_SignalDown" runat="server"><a class="expanded"> &nbsp;  &nbsp; &nbsp; Original Loading Signals Attributes</a></h4>
                      <div style="float:left; width:100%">
                      <div  runat="server" id="Div26"></div>
                    <asp:GridView ID="DownloadOLS_Signal_GrdV" runat="server" CellPadding="4" AutoGenerateColumns="False" OnRowCommand="OnRowCommand" visible = "false" 
                        CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"  DataKeyNames="LocalID" OnDataBound="GridView1_DataBound1"
                            DataSourceID="SelectDown_OLS_Signal" ForeColor="#333333" 
                             Font-Size="0.85em" 
                            GridLines="None" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                            
                           
                                <asp:BoundField  DataField="PrTitle" HeaderText="Project Title"/>
                                <asp:BoundField  DataField="specName" HeaderText="Specimen Name"/>
                                <asp:BoundField  DataField="CompExpName" HeaderText="CompExp Name"/>
                                <asp:BoundField  DataField="DLCName" HeaderText="DLC Name"/>
                                <asp:BoundField  DataField="OLSName" HeaderText="OLS Name"/>
                                <asp:BoundField  DataField="Sig_Name" HeaderText="Signal Name"/>
                                <asp:BoundField  DataField="Sig_Attr" HeaderText="Signal Attribute"/>
                                <asp:BoundField  DataField="Sig_PQ" HeaderText="Signal Physical Quantity"/>
                                <asp:BoundField  DataField="Sig_Type" HeaderText="Signal Type"/>
                                <asp:BoundField  DataField="Sig_Unit" HeaderText="Signal Unit"/>
                                 <asp:BoundField  DataField="Sig_Location" HeaderText="Signal Location"/>
                                
                                <asp:BoundField Visible="false"  DataField="LocalID" HeaderText="LocalId"/>
              
                                 <asp:TemplateField ItemStyle-Width="30px" HeaderText="Download">
                                   <ItemTemplate>
                                    <asp:ImageButton ID="ImgBtn" runat="server" Width="30px" Height="30px"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  ImageUrl="~/images/download blue.png"/>
                                   </ItemTemplate>
                                </asp:TemplateField>
                        
                           </Columns>
                        </asp:GridView>
                          <asp:SqlDataSource ID="SelectDown_OLS_Signal" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT project.title AS PrTitle, specimen.Name AS specName, CompExp.Name AS CompExpName, dlc.Name AS DLCName, originalloading.name as OLSName,inputsignal.Label as Sig_Name,inputsignal.Attribute as  Sig_Attr,inputsignal.PhysicalQ as Sig_PQ,inputsignal.type as Sig_Type,inputsignal.Unit as Sig_Unit,inputsignal.Location as Sig_Location,inputsignal.repetition, inputsignal.localid as LocalID FROM project, specimen, compexp, dlc, originalloading,compexp_has_dlc, nominalloading_originalloading,inputsignal WHERE (project.idproject = @idproject and project.idproject = specimen.project_idproject and specimen.idspecimen = compexp.specimen_idspecimen and compexp_has_dlc.CompExp_idCompExp= compexp.idcompexp and compexp_has_dlc.dlc_iddlc = dlc.iddlc and nominalloading_originalloading.NominalLoading = dlc.iddlc and nominalloading_originalloading.originalLoading = OriginalLoading.idOriginalLoading and OriginalLoading.signal_idsignal = inputsignal.idInputSignal)"
                            OnSelecting="_InitialProjectSelecting">
             <SelectParameters>
             <asp:Parameter Name = "idproject" />
             </SelectParameters>
                        </asp:SqlDataSource>
                
                
                         </div>
                       
                        
                        <br />
                        <asp:Button visible ="false"  ID="BtnDownLoad" CssClass="button" runat="server" OnClick="BtnDownload_Click" Text="Download" />     


                          <br />      
                      
            </asp:View>   
                <asp:View ID="DownloadNoItems_View" runat="server">
                
                <div id="NoData" class="rbroundbox">
	<div class="rbtop"><div></div></div>
		<div class="rbcontent"><p>There are no downloadable items on this Project</p>
		</div><!-- /rbcontent -->
	<div class="rbbot"><div></div></div>
</div><!-- /rbroundbox -->
                </asp:View>
                </asp:MultiView>
            
        </div>
        
        
          
        </div>                   
			    <!-- #rightTopPane-->
			    <!-- #rightBottomPane--></div>  	    
		 <div id="SearchDiv">
           <!--Content-->
         <br />
           <div class="SearchToolTip" >
           Tip:To select more than one values in the same category press and hold ctrl. To unselect a value hold ctrl and click the value.
           </div>
           
             <br /> <br />
           <div id="SearchSlick">
                       <br />    
            <div class="demo-show">
            
            <h4 id="h4SearchPr" runat="server"><a class="expanded">&nbsp; &nbsp; &nbsp;  Project</a></h4>
           
           <div id="DivSearchPr" runat="server" class="category">
           <div  class="LabelAndListBox">
              <label class="Label">location</label><br />
              <asp:ListBox runat="server" class="ListBox" ID="ListBox_Location" SelectionMode="Multiple"></asp:ListBox>
           </div>
           <div class="space"><p></p>
           </div>
           <div  class="LabelAndListBox">
              <label class="Label">Infrastructure</label><br />
              <asp:ListBox runat="server" class="ListBox" ID="ListBox_Infrastructure" SelectionMode="Multiple"></asp:ListBox>
           </div>
           <div  class="space"><p></p>
           </div>
           <div class="LabelAndListBox">
              <label class="Label">Research Area</label><br />
              <asp:ListBox runat="server" class="ListBox" ID="ListBox__ResearchArea"  SelectionMode="Multiple"></asp:ListBox>
             </div>  
           <div  class="space"><p></p>
           </div>     
             <div class="LabelAndListBox">
              <label class="Label">Principle Experiment type</label><br />
              <asp:ListBox runat="server" class="ListBox" ID="ListBox_PrincipleExpType" SelectionMode="Multiple"></asp:ListBox>
              </div>
              <div class="space"><p></p>
           </div>
               <div class="LabelAndListBox">
              <label class="Label">Acronym</label><br />
              <asp:ListBox runat="server" class="ListBox" ID="ListBox_Acronym"  SelectionMode="Multiple"></asp:ListBox>
              </div>
              <div class="space"><p></p>
           </div>

            <div class="LabelAndListBox">
              <label class="Label">Investigator</label><br />
              <asp:ListBox runat="server" class="ListBox" ID="ListBox_Investigator"  SelectionMode="Multiple"></asp:ListBox>
              </div>
              <div class="space"><p></p>
           </div>
    </div>
      
      <h4 id="h4SearchSpec" runat="server"><a class="expanded"> &nbsp; &nbsp; &nbsp; Specimen</a></h4>
    <div id="DivSearchSpec" runat="server"  class="category">
    <div class="LabelAndListBox">
              <label class="Label">Structural System/Element</label><br />
              <asp:ListBox runat="server" class="ListBox" ID="ListBox_StructuralElement" SelectionMode="Multiple"></asp:ListBox>
             </div>
             <div class="space"><p></p>
           </div>
           
           <div class="LabelAndListBox">
              <label class="Label">Structural Material</label><br />
              <asp:ListBox runat="server" class="ListBox" ID="ListBox_Material" SelectionMode="Multiple"></asp:ListBox>
             </div>
             <div class="space"><p></p>
           </div>
           
           
         
    </div>

      <h4 id="h4SearchCompExp" runat="server"><a class="expanded"> &nbsp; &nbsp; &nbsp;  Experiment</a></h4>
    <div  id="DivSearchCompExp" runat="server" class="category">
    <br />
     <div class="LabelAndListBox">
              <label class="Label">Experiment Type</label><br />
              <asp:ListBox runat="server" class="ListBox" ID="ListBox_ExpType"  SelectionMode="Multiple"></asp:ListBox>
             </div>
             <div class="space"><p></p></div>
         
           <div class="LabelAndListBox">
              <label class="Label">Nature</label><br />
              <asp:ListBox runat="server" class="ListBox" ID="ListBox_ExpOLS_Nature" SelectionMode="Multiple"></asp:ListBox>
             </div>
             <div class="space"><p></p></div>
             
             <div class="LabelAndListBox">
              <label class="Label">Loading Source</label><br />
              <asp:ListBox runat="server" class="ListBox" ID="ListBox_ExpOLS_Source"  SelectionMode="Multiple"></asp:ListBox>
             </div>
             <div class="space"><p></p></div>
              
            
    
         
    </div>
   
   
    <h4 id="h1" runat="server"><a class="expanded"> &nbsp; &nbsp; &nbsp;Computation</a></h4>
    <div  id="Div27" runat="server" class="category">
    <br />
    <div style=" width:1%;float:left;height:80%; "><p></p></div>
    <div class="LabelAndListBox">
              <label class="Label">Computation Type</label><br />
              <asp:ListBox runat="server" class="ListBox" ID="ListBox_CompType" SelectionMode="Multiple"></asp:ListBox>
              </div>
              <div class="space"><p></p>
           </div>
           
             
             <div class="LabelAndListBox">
              <label class="Label">Computation Software</label><br />
              <asp:ListBox runat="server" class="ListBox" ID="ListBox_CompSoft"  SelectionMode="Multiple"></asp:ListBox>
             </div>
             <div class="space"><p></p></div>
              
              
         
    </div>
    
      <h4 id="h4SearchSignal" runat="server"><a class="expanded"> &nbsp; &nbsp; &nbsp;  Signal</a></h4>
           <div id="DivSearchSignal" runat="server" class="category">
           
        <div class="LabelAndListBox">
              <label  class="Label">Physical Q</label><br />
              <asp:ListBox runat="server" class="ListBox" ID="ListBox_Signal_PhysicalQ"  SelectionMode="Multiple"></asp:ListBox>
           </div>
           <div class="space"><p></p>
           </div>
           <div style=" width:1%;float:left;height:100%;"><p></p>
           </div>
             
    </div>

    </div>
    </div>
           <div style="width:100%;">
    <center>
     <asp:Button CssClass="button" ID="Button1" runat="server" onclick="NewSearchFunction"  Text="Search" />
     </center>
    </div>
     	    
	    <!-- #rightSplitterContainer--></div>
        <div id="FreeSearch" runat ="server">
        <br />
          <div class="SearchToolTip" >
           Tip: Use double quotes in a set of words if you want the exact words in the exact order(e.g. "Structural Dynamics Laboratories").Use * for any unknown keywords(e.g. structural* will return structural dynamics laboratory and 
structural performance-deficient but structural * laboratory will return only structural dynamics laboratory ) 
           Use AND if you want all the words to included in your search results (e.g. PERC AND PEM). Use the OR operator if you want either one of several words(e.g. PERC AND PEM). 
           
           </div>
        <br />
                 <asp:LinkButton runat="server" ID="LnkBtnAdvSearch" style="float:right;" OnClick="LnkBtnAdvSearch_Click">Structured Search</asp:LinkButton>
         <br /><br />
         <div id="OpenSearchDiv">
         <input type="text" id= "Txt_FreeSearch" runat="server"/>
          <asp:ImageButton ImageUrl="~/images/Search.png" ID="ImageButton1"  runat="server" OnClick="BtnFreeSearch"  />
          <br />
 
         
         </div>
         <br /><br /><br /><br />
           
	    
         </div>
         <div  runat="server" id="Search_Criteria"></div>
        
         <div runat="server" id="GoogleDivs"></div>
	     <div id="HomeDiv">
	  <div class="HomeContent">
	  <h2>About SERIES Data Access Portal</h2>
	
      <br />
      
             <div class="container1">
	        
      <div id="about"  style=" float:left; width:50%;">
      
     
      
      <p> The creation of the distributed database aims to improve the dissemination and use of experimental 
     results and to foster the impact of earthquake engineering research on practice, innovation and 
     earthquake risk mitigation.This requires harmonisation and unification of the European databases 
     in earthquake engineering and the possibility of accessing, through a unique portal, 
     the data stored at different database nodes which are able to dialog with the central portal using 
     a common communication protocol.</p>
     
   
     <blockquote class="blockquote">
    
     <p style="font-style: italic;">"The mission of the Data Access Portal is to foster a sustainable culture of co-operation among all of the research infrastructures and teams that are
active in earthquake engineering in Europe"</p>
</blockquote>
 This requires harmonisation and unification of the European databases 
     in earthquake engineering and the possibility of accessing, through a unique portal, 
     the data stored at different database nodes which are able to dialog with the central portal using 
     a common communication protocol.
          
      </div>
	<div class="pics">
		<img alt="Upatras" width ="400" height="300" src="images/Eucentre-TREES-Lab-controllingCameras.jpg" />
		<img alt="LNEC 1"width ="400" height="300" src="images/Eucentre-TREES-Lab-IMG_6396.jpg" />
		<img alt="Upatras"width ="400" height="300" src="images/LNEC4_600_0.jpg" />
		<img alt="LNEC 1" width ="400" height="300" src="images/Eucentre-TREES-Lab-buildingOnTable.jpg" />
		<img alt="Upatras" width ="400" height="300" src="images/Eucentre-TREES-Lab-IMG_6162.jpg" />
		<img alt="LNEC 1" width ="400" height="300" src="images/fig2_ed_0.jpg" />
		<img alt="Upatras" width ="400" height="300" src="images/JRC2.jpg" />
		<img alt="LNEC 1" width ="400" height="300" src="images/JRC1.jpg" />
		<img alt="Upatras" width ="400" height="300" src="images/fig2_ed.jpg" />
		<img alt="LNEC 1" width ="400" height="300" src="images/fig5_ed_2.jpg" />
	
	
	</div>

</div>


<div id="News">
    

          <div class="c25l">
            <div class="subcl">
              <!-- Insert your subtemplate content here -->
              
              <h3>Recently Published project</h3>
              <div id="RecPubPr" runat="server"></div>
            
              
            </div>
          </div>
          <div class="c50l">
            <div class="subc">
              <!-- Insert your subtemplate content here -->
              <h3>Exchage Data Format</h3>
              <p>
The European scientific community is currently fragmented with each laboratory holding
experimental data with a unique local data model and user interface, language and scheme. As a
consequence, the dissemination and use of these experimental results outside of the laboratory
where they are produced can be problematic. To overcome this, it is proposed to add a layer on
top of the existing local databases that is accessible through a unique Data Access Portal. The
aim is not to build a central database where local databases either migrate or merge but instead to
provide centralised access to database nodes that are distributed over the network which are able
to dialog with a central portal in a uniform manner. For achieving the aformentionoed goal a common
Exchange Data Format is needed 
     </p>
              
              <p><a href="javascript:EDF_ShortDescription()">••• more</a></p>
            </div>
          </div>
          <div class="c25r">
            <div class="subcr">
              <!-- Insert your subtemplate content here -->
              <h3>User Manual</h3>
              <p>
From a conceptual point of view the Data Access Portal has been designed to act as an
information space. Taking  the above into consideration Data Access Portal supports two complementary modes of information retrieval: a) direct navigation mechanism which allows users to
browse through the published projects by interacting with a tree view control which is always visible at the left side of the web application. This panel contains all the available published
projects which are structured according to the Exchanged Data Format b) direct search functionality which is a structured keyword-based search where can be found specific information based on exchange data format keywords </p>
             <p><a href="javascript:window.open('http://www.series.upatras.gr/sites/default/files/Deliverable_D2.3.pdf','mywindow','width=600,height=800,scrollbars=yes');">••• more</a></p>
            </div>
          </div>
     
  </div>

      </div>



	  </div>
	     <div id="LabInfoDiv">
	    
	   
	    
	    <asp:GridView ID="GridLabInfo" runat="server" CellPadding="4" AutoGenerateColumns="False" Visible="false"
				     CssClass="mGrid" PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"
                            DataSourceID="SelectLabInfo" ForeColor="#333333" GridLines="None" 
                            Font-Size="0.85em" HorizontalAlign="Center">
                            <RowStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                              <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                            <asp:ImageButton runat="server" ID="ImgBtn" OnClientClick=<%# "window.open('" + (((System.Data.DataRowView)Container.DataItem)["siteurl"]) + "');return false;" %>  ImageUrl= <%#"~/images/" + (((System.Data.DataRowView)Container.DataItem)["name"]) +".jpg"%> />
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField  DataField="LastUpdateTime" HeaderText="LastUpdateTime"/>
                            <asp:BoundField  DataField="LongName" HeaderText="Partner"/>
                            <asp:BoundField  DataField="Name" HeaderText="ShortName"/>			    		
                    	    <asp:BoundField  DataField="Country" HeaderText="Country"/>
                    	    <asp:BoundField  DataField="PrNum" HeaderText="Public Projects"/>
                    	   
                    </Columns>


                        </asp:GridView>
                        <asp:SqlDataSource ID="SelectLabInfo" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:newseriesserverConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:newseriesserverConnectionString.ProviderName %>" 
                            SelectCommand="SELECT laboratory.LongName, laboratory.name, laboratory.lastupdatetime, laboratory.country, laboratory.SiteUrl, Count(project.idproject) as PrNum  FROM laboratory,project WHERE (project.laboratory_idlaboratory =laboratory.idlaboratory and idlaboratory= @LabID)"
                            OnSelecting="_LabSelecting">
             <SelectParameters>
             <asp:Parameter Name = "LabID" />
             </SelectParameters>
                        </asp:SqlDataSource>
	  
	  </div>
	<!-- #rightPane --></div>
	</div>

	</div>
	  <div class="clearfooter"></div>
</div>
<!-- #splitterContainer -->

<div id="bottom-wrapper" class="region-bottom">
			<div id="bottom-wrapper-inner">
				<div class="subcolumns">
					 <div id="bottomleft">
							
                            <div class="content">
                                 <p></p>
                            </div>

  

							  </div> <!-- /#bottomLeft -->
												</div>			
				<div class="subcolumns">										
											<div id="bottomright" >
                                                
  
                                                <div class="content">
                                                    <div>
                                                    </div>
                                                </div>
											</div> <!-- /#bottomRight -->
								</div>
						
				
			
		</div>	

		</div>	

   </form>
</body>
</html>
