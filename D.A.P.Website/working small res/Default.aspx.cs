using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;


namespace SERIESnew_db
{
    public partial class _Default : System.Web.UI.Page
    {

        MySqlConnection conn = new MySqlConnection("server=localhost;User Id=root;database=newseriesserver;password=root;");
        int NumPrRes = 0;
        int NumSpecRes = 0;
        int NumCompExpRes = 0;
        int NumSigRes = 0;
        List<int> PrIds = new List<int>();
        SearchItem Initial = new SearchItem("Initial", -1);
        int TreeNodesCount = 0;
        int CountPrResults = 1;
        TreeNode ClickedNode = null;
        List<SearchItem> PrIdsList = new List<SearchItem>();
        string ValuePathSeparator = null;

        /*Session Panel_State:describes the state of the panel.
         * default:the home page where nothing has clicked
         *Search:Active if and only if search button clicked 
         *(User perform search or user has already perform a search and returns back via javascript)
         *Tabs a treeNode has been clicked
         */

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MovetoTheNextPanelState(null, "NotPostback");
                Session["LeftState"] = "P";
                Session["ClickedTree"] = "PublicProjects";

                string SelectResource = "select DISTINCT  keyword from Search where category = 'Resource'";
                string SelectInfrastructure = "select DISTINCT  keyword from Search where category = 'Infrastructure'";
                string SelectLocation = "select DISTINCT  keyword from Search where category = 'location'";
                string SelectResearchArea = "select DISTINCT  keyword from Search where category = 'ResearchArea'";
                //string SelectProjectStatus = "SELECT DISTINCT  status FROM Project where Status is not null and status != ' ' ORDER BY status ";
                string SelectPrincipleExpType = "select DISTINCT  keyword from Search where category = ''";

                string SelectStructuralElement = "select DISTINCT  keyword from Search where category = 'structural element'";
                string SelectMaterial = "select DISTINCT  keyword from Search where category = 'Resource'";
               // string SelectSimilitude = "SELECT DISTINCT scaledPropertyName FROM SIMILITUDE where scaledPropertyName is not null and scaledpropertyname != ' ' ORDER BY scaledPropertyName ";
               // string SelectSpecimenDocs = "SELECT DISTINCT type FROM specimenreport where type is not null and type != ' ' ORDER BY type ";
                string SelectSpecimenPics = "select DISTINCT  keyword from Search where category = 'Resource'";

                //string SelectExpType = "SELECT DISTINCT Attribute FROM exptype";
                string SelectExpType = "select DISTINCT  keyword from Search where category = 'exptype'";
                string SelectExp_OLS_Nature = "select DISTINCT  keyword from Search where category = 'OLS_nature'";
                string SelectExp_OLS_Source = "select DISTINCT  keyword from Search where category = 'OLS_Source'";

                string SelectCompType = "select DISTINCT  keyword from Search where category = 'CompType'";
               // string SelectComp_SymmetryType = "SELECT DISTINCT SymmetryType FROM MeshModel where symmetryType is not null and symmetryType != ' '  ORDER BY SymmetryType ";
               //string SelectComp_Nonlinearity = "SELECT DISTINCT Nonlinearity FROM MeshModel where NonLinearity is not null and NonLinearity != ' ' ORDER BY Nonlinearity ";

                //string SelectSignal_Attr = "SELECT DISTINCT Attribute FROM signalresult where Attribute is not null and attribute != ' ' ORDER BY Attribute ";
                string SelectSignal_PhysicalQ = "select DISTINCT  keyword from Search where category = 'PhysicalQ'";
               // string SelectSignal_Unit = "SELECT DISTINCT Unit FROM SignalResult where Unit is not null and Unit != ' ' ORDER BY Unit";
               // string SelectSignal_Location = "SELECT DISTINCT Location FROM SignalResult where Location is not null and Location != ' ' ORDER BY Location ";

                string SelectInvestigator = "select DISTINCT  keyword from Search where category = 'investigator'";
                string SelectAcronym = "select DISTINCT  keyword from Search where category = 'acronym'";
                string SelectCompSoft = "select DISTINCT  keyword from Search where category = 'CompSoft'";

                MySqlConnection conn = new MySqlConnection("server=localhost;User Id=root;database=newseriesserver;password=root;");
                MySqlCommand cmd = new MySqlCommand(SelectResource, conn);
              

                
                cmd.CommandText = SelectInfrastructure;
                conn.Open();
                ListBox_Infrastructure.DataSource = cmd.ExecuteReader();
                ListBox_Infrastructure.DataTextField = "keyword";
                ListBox_Infrastructure.DataValueField = "keyword";
                ListBox_Infrastructure.DataBind();
                conn.Close();

                cmd.CommandText = SelectLocation;
                conn.Open();
                ListBox_Location.DataSource = cmd.ExecuteReader();
                ListBox_Location.DataTextField = "keyword";
                ListBox_Location.DataValueField = "keyword";
                ListBox_Location.DataBind();
                conn.Close();


                cmd.CommandText = SelectResearchArea;
                conn.Open();
                ListBox__ResearchArea.DataSource = cmd.ExecuteReader();
                ListBox__ResearchArea.DataTextField = "keyword";
                ListBox__ResearchArea.DataValueField = "keyword";
                ListBox__ResearchArea.DataBind();
                conn.Close();

                
                cmd.CommandText = SelectPrincipleExpType;
                conn.Open();
                ListBox_PrincipleExpType.DataSource = cmd.ExecuteReader();
                ListBox_PrincipleExpType.DataTextField = "keyword";
                ListBox_PrincipleExpType.DataValueField = "keyword";
                ListBox_PrincipleExpType.DataBind();
                conn.Close();

                cmd.CommandText = SelectStructuralElement;
                conn.Open();
                ListBox_StructuralElement.DataSource = cmd.ExecuteReader();
                ListBox_StructuralElement.DataTextField = "keyword";
                ListBox_StructuralElement.DataValueField = "keyword";
                ListBox_StructuralElement.DataBind();
                conn.Close();


                cmd.CommandText = SelectMaterial;
                conn.Open();
                ListBox_Material.DataSource = cmd.ExecuteReader();
                ListBox_Material.DataTextField = "keyword";
                ListBox_Material.DataValueField = "keyword";
                ListBox_Material.DataBind();
                conn.Close();

                

                

                cmd.CommandText = SelectSpecimenPics;
                //conn.Open();

                //ListBox_Spec_Picture.DataSource = cmd.ExecuteReader();
                //ListBox_Spec_Picture.DataTextField = "type";
                //ListBox_Spec_Picture.DataValueField = "type";
                //ListBox_Spec_Picture.DataBind();
                //conn.Close();


                cmd.CommandText = SelectExpType;
                conn.Open();

                ListBox_ExpType.DataSource = cmd.ExecuteReader();
                ListBox_ExpType.DataTextField = "keyword";
                ListBox_ExpType.DataValueField = "keyword";
                ListBox_ExpType.DataBind();
                conn.Close();


                cmd.CommandText = SelectExp_OLS_Nature;
                conn.Open();

                ListBox_ExpOLS_Nature.DataSource = cmd.ExecuteReader();
                ListBox_ExpOLS_Nature.DataTextField = "keyword";
                ListBox_ExpOLS_Nature.DataValueField = "keyword";
                ListBox_ExpOLS_Nature.DataBind();
                conn.Close();


                cmd.CommandText = SelectExp_OLS_Source;
                conn.Open();

                ListBox_ExpOLS_Source.DataSource = cmd.ExecuteReader();
                ListBox_ExpOLS_Source.DataTextField = "keyword";
                ListBox_ExpOLS_Source.DataValueField = "keyword";
                ListBox_ExpOLS_Source.DataBind();
                conn.Close();

                cmd.CommandText = SelectCompType;
                conn.Open();

                ListBox_CompType.DataSource = cmd.ExecuteReader();
                ListBox_CompType.DataTextField = "keyword";
                ListBox_CompType.DataValueField = "keyword";
                ListBox_CompType.DataBind();
                conn.Close();


                
                //###############
                cmd.CommandText = SelectSignal_PhysicalQ;
                conn.Open();

                ListBox_Signal_PhysicalQ.DataSource = cmd.ExecuteReader();
                ListBox_Signal_PhysicalQ.DataTextField = "keyword";
                ListBox_Signal_PhysicalQ.DataValueField = "keyword";
                ListBox_Signal_PhysicalQ.DataBind();
                conn.Close();

                cmd.CommandText = SelectInvestigator;
                conn.Open();

                ListBox_Investigator.DataSource = cmd.ExecuteReader();
                ListBox_Investigator.DataTextField = "keyword";
                ListBox_Investigator.DataValueField = "keyword";
                ListBox_Investigator.DataBind();
                conn.Close();


                cmd.CommandText = SelectAcronym;
                conn.Open();

                ListBox_Acronym.DataSource = cmd.ExecuteReader();
                ListBox_Acronym.DataTextField = "keyword";
                ListBox_Acronym.DataValueField = "keyword";
                ListBox_Acronym.DataBind();
                conn.Close();



                cmd.CommandText = SelectCompSoft;
                conn.Open();

                ListBox_CompSoft.DataSource = cmd.ExecuteReader();
                ListBox_CompSoft.DataTextField = "keyword";
                ListBox_CompSoft.DataValueField = "keyword";
                ListBox_CompSoft.DataBind();
                conn.Close();


                PrTitleLabel.Text = null;
                conn.Open();
                MySqlCommand selectAllProjects = new MySqlCommand("select count(idProject) from Project", conn);
                MySqlDataReader AllPrReader = selectAllProjects.ExecuteReader();
                if (AllPrReader.Read())
                    LblPublicProjects.Text = AllPrReader[0].ToString();
             
                conn.Close();



            }
            else
            {
                if ((Session["ActiveView"] == null) && (Session["ClickedTree"] == null)&&(Session["LeftState"]==null))
                    Response.Redirect("http://150.140.134.43/dapnewdb");
            }





        }



        
        //_SpecAllSelecting
        //CHECK TO DELETE ALL THIS FUNCTION INSTEAD OF ONE
        //ALSO DELETE THE IFs INSIDE EACH FUNCTION USING THE SENDER OBJECT
        //find the clicked node
        protected void _LevelAllSelecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

            string ctrlname = Page.Request.Params.Get("__EVENTTARGET");
            e.Command.Parameters["idproject"].Value = FindProjectID()[1]; // id[1];
            if (ctrlname.Equals("ProjectMenu") && ProjectMenu.SelectedItem.Text.Contains("Computation"))
                e.Command.Parameters["type"].Value = "Computation";
            else if (ctrlname.Equals("ProjectMenu") && ProjectMenu.SelectedItem.Text.Contains("Experiment"))
                e.Command.Parameters["type"].Value = "Experiment";
            else if (ctrlname.Contains("Tree") && (ClickedNode.Text.Equals("Computation Level") || ClickedNode.Parent.Text.Equals("Computation Level") || ClickedNode.Parent.Parent.Text.Equals("Computation Level")))
                e.Command.Parameters["type"].Value = "Computation"; // id[1];
            else if (ctrlname.Contains("Tree") && (ClickedNode.Text.Equals("Experiment Level") || ClickedNode.Parent.Text.Equals("Experiment Level") || ClickedNode.Parent.Parent.Text.Equals("Experiment Level")))
                e.Command.Parameters["type"].Value = "Experiment"; // id[1];
            

        }
        
        //find the project id
        protected string[] FindProjectID()
        {
            string[] ProjectID = null;
            string ProjectTitle = PrTitleLabel.Text.Remove(0,17);

            string ctrlname = Page.Request.Params.Get("__EVENTTARGET");
            if (ctrlname.Contains("Tree"))
            {
                TreeView TempTreeview = (TreeView)this.Page.FindControl(ctrlname);



                if (TempTreeview.SelectedNode.Text.Equals(ProjectTitle))
                    ProjectID = TempTreeview.SelectedNode.Value.Split(':');
                else if (TempTreeview.SelectedNode.Parent.Text.Equals(ProjectTitle))
                    ProjectID = TempTreeview.SelectedNode.Parent.Value.Split(':');
                else if (TempTreeview.SelectedNode.Parent.Parent.Text.Equals(ProjectTitle))
                    ProjectID = TempTreeview.SelectedNode.Parent.Parent.Value.Split(':');
                else if (TempTreeview.SelectedNode.Parent.Parent.Parent.Text.Equals(ProjectTitle))
                    ProjectID = TempTreeview.SelectedNode.Parent.Parent.Parent.Value.Split(':');
            }
            else
            {
                ProjectID = ProjectMenu.SelectedItem.Value.Split(':');
              
            }
      
            return ProjectID;

        }
 
        protected void _ProjectSelecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            //string[] id;
            //if (Page.Request.Params.Get("__EVENTTARGET").Equals("SearchTree"))
            //{
             //   id = SearchTree.SelectedNode.Value.Split(':');
           // }
            //else 
             //   id = TreeView2.SelectedNode.Value.Split(':');

        
            //e.Command.Parameters["idproject"].Value = id[1];

            string ctrlname = Page.Request.Params.Get("__EVENTTARGET");
            e.Command.Parameters["idproject"].Value = FindProjectID()[1]; // id[1];
            



        }
        protected void _SpecimenSelecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            string[] id;
            if (Page.Request.Params.Get("__EVENTTARGET").Equals("SearchTree"))
            {
                id = SearchTree.SelectedNode.Value.Split(':');
            }
            else
                id = TreeView2.SelectedNode.Value.Split(':');
            e.Command.Parameters["idspecimen"].Value = id[1];



        }
        protected void _CompExpSelecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            string[] id;
            if (Page.Request.Params.Get("__EVENTTARGET").Equals("SearchTree"))
            {
                id = SearchTree.SelectedNode.Value.Split(':');
            }
            else
                id = TreeView2.SelectedNode.Value.Split(':');
            e.Command.Parameters["idcompexp"].Value = id[1];
            
            //e.Command.Parameters["type"].Value = 
        }
        protected void _SignalSelecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            string[] id;
            if (Page.Request.Params.Get("__EVENTTARGET").Equals("SearchTree"))
            {
                id = SearchTree.SelectedNode.Value.Split(':');
            }
            else
                id = TreeView2.SelectedNode.Value.Split(':');
            e.Command.Parameters["idsignal"].Value = id[1];

        }
        protected void _LabSelecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            string[] id;
            
            if (Page.Request.Params.Get("__EVENTTARGET").Equals("SearchTree"))
            {
                id = SearchTree.SelectedNode.Value.Split(':');
            }
            else if (Page.Request.Params.Get("__EVENTTARGET").Equals("PartnerTree"))
            {
                id = PartnerTree.SelectedNode.Value.Split(':');
            }
            else
                id = TreeView2.SelectedNode.Value.Split(':');
            e.Command.Parameters["LabID"].Value = id[1];
        }

        protected void _InitialProjectSelecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            string ttt = sender.GetType().ToString();
            TreeView TempTree;
            string[] id;
            if (Page.Request.Params.Get("__EVENTTARGET").Equals("SearchTree"))
            {
                TempTree = SearchTree;
                id = SearchTree.SelectedNode.Value.Split(':');
            }
            else if (Page.Request.Params.Get("__EVENTTARGET").Equals("TreeView2"))
            {
                TempTree = TreeView2;
                id = TreeView2.SelectedNode.Value.Split(':');
            }
            else 
            {
                TempTree = PartnerTree;
                id = PartnerTree.SelectedNode.Value.Split(':');

            }


            switch (id[0])
            {
                case "ProjectID":
                    id = TempTree.SelectedNode.Value.Split(':');
                    break;
                case "SpecimenID":
                    id = TempTree.SelectedNode.Parent.Value.Split(':');
                    break;
                case "CompExpID":
                    id = TempTree.SelectedNode.Parent.Parent.Value.Split(':');
                    break;
                case "SignalID":
                    id = TempTree.SelectedNode.Parent.Parent.Parent.Value.Split(':');
                    break;
                default:
                    id = TempTree.SelectedNode.Value.Split(':');
                    break;
            
            }



            MySqlConnection conn = new MySqlConnection("server=localhost;User Id=root;database=newseriesserver;password=root;");
            conn.Open();
            MySqlCommand selectPr = new MySqlCommand("select * from Project where idProject = @idproject", conn);
            selectPr.Parameters.AddWithValue("@idproject", id[1]);
            MySqlDataReader PrReader =  selectPr.ExecuteReader();
            if (PrReader.HasRows)
            {
                
                PrReader.Read();
                Lbl_PrTitleCont.Text = PrReader["Title"].ToString();
                Lbl_SponsorContent.Text = PrReader["Sponsor"].ToString();
                Lbl_StartDateContent.Text = PrReader["StartDate"].ToString();
                Lbl_EndDateContent.Text = PrReader["EndDate"].ToString();
                Lbl_AcronimContent.Text = PrReader["Acronym"].ToString();
                PrDescription.InnerText = PrReader["Reason"].ToString();
            }

            conn.Close();
            //if comes here from specimen,compexp,signal node it is trigger by the download tab where I need the projectID
            /*
                if (Page.Request.Params.Get("__EVENTTARGET").Equals("SearchTree"))
                {
                }
             */
            e.Command.Parameters["idproject"].Value = id[1];

        }
        
        protected void loadData(object sender, EventArgs e)
        {

            TreeView test = (TreeView)sender;
            
            //take the node to use it for changing the gridview rows style
            ClickedNode = test.SelectedNode;
           
            
            /*
            //if it is visible it has been performed a search
            if (SearchTree.Visible == true)
            {//if search tree is null show only the first tree,if it is not null you have to show it  both of them
                Session["ClickedTree"] = "SearchTree";
            }
            */

            //Response.Write (Request.Form["__EVENTTARGET"]) ;

            if(test.SelectedNode.Text.Equals("Specimen Level"))
            {
                MovetoTheNextPanelState(null, "LoadData_General");
                DataBindDownloadView();

                PrTitleLabel.Text = "Current project: ";
                PrTitleLabel.Text += test.SelectedNode.Parent.Text;
                PrIDLabel.Text = test.SelectedNode.Parent.Value;
                
                MultiView1.ActiveViewIndex = 6;

                DataBindGridView(GridSpecLevelSpecData, H4SpecData, "Specimen Data");
                DataBindGridView(GridSpecLevelSpecDocs, H4SpecDoc, "Specimen Document");
                DataBindGridView(GridSpecLevelSpecIMGs, H4SpecImg, "Specimen Images");
                DataBindGridView(GridSpecLevelSpecScaling, H4SpecScaling, "Specimen Scaling");
                DataBindGridView(GridSpecLevelStrElemData, H4SpecStrElem, "Structural Element Data");
                DataBindGridView(GridSpecLevelStrElemMatActProp, H4StrlElem_Mat_ActualProp, "Structual Element and Material Actual Properties");
                DataBindGridView(GridSpecLevelStrElemMaterial, H4SpecStrElemMat, "Structural Element and it's Material");
                DataBindGridView(GridSpecLevelStrElemMatNomProp, H4StrlElem_Mat_Nom_Prop, "Structural Elements and material Nominal Properties");
                
                FindSpecimenLevelView();

                ProjectMenu.Items[1].Selected = true;
                ProjectMenu.Items[0].Value = "Project" + test.SelectedNode.Parent.Value;
                ProjectMenu.Items[1].Value = "Specimen" + test.SelectedNode.Parent.Value;
                ProjectMenu.Items[2].Value = "Experiment" + test.SelectedNode.Parent.Value;
                ProjectMenu.Items[3].Value = "Computation" + test.SelectedNode.Parent.Value;
                ProjectMenu.Items[4].Value = "Signal" + test.SelectedNode.Parent.Value;
               
            }
            else if (test.SelectedNode.Text.Equals("Experiment Level"))
            {
                DataBindDownloadView();
                MovetoTheNextPanelState(null, "LoadData_General");

                PrTitleLabel.Text = "Current project: ";
                PrTitleLabel.Text += test.SelectedNode.Parent.Text;
                PrIDLabel.Text = test.SelectedNode.Parent.Value;


                MultiView1.ActiveViewIndex = 7;
                DataBindGridView(GridCompExpLevelPerson, H4ExpCompPerson, "Experiment Personnel ");
                DataBindGridView(GridCompExpLevelData, H4ExpCompData, "Experiment data");
                DataBindGridView(GridCompExpLevelDLC, H4ExpCompDLC, "Experiment Detailed Loading Characteristics");
                DataBindGridView(GridCompExpLevelDOC, H4ExpCompDoc, "Experiment Documents");
                DataBindGridView(GridCompExpLevelIMG, H4ExpCompIMGS, "Experimen Imaegs");
                DataBindGridView(GridCompExpLevelMeshModel, H4ExpCompMeshModel, "Mesh model");
                DataBindGridView(GridCompExpLevelMeshModelDoc, H4ExpCompMeshModelDoc, "Mesh Model Documents");
                DataBindGridView(GridCompExpLevelMeshModelIMG, H4ExpCompMeshModelIMG, "Mesh Model Images");
                DataBindGridView(GridCompExpLevelOLS, H4ExpCompOLS, "Original Loading Signals");
                DataBindGridView(GridCompExpLevelPerson,H4ExpCompPerson,"Experiment Imvestigators");
                DataBindGridView(GridCompExpLevelVideo,H4ExpCompVideo,"Experiment Video");
                DataBindGridView(GridCompExpLevelPC, H4ExpCompPC, "Computer System");
                DataBindGridView(GridCompExpLevelInputFile, H4ExpCompInputFile, "Effective Input Files");
                DataBindGridView(GridCompExpLevelOLS_Signals, H4ExpCompOLS_Signal, "OLS signal Attributes");
                FindCompExpLevelView();

                ProjectMenu.Items[2].Selected = true;
                ProjectMenu.Items[0].Value = "Project" + test.SelectedNode.Parent.Value;
                ProjectMenu.Items[1].Value = "Specimen" + test.SelectedNode.Parent.Value;
                ProjectMenu.Items[2].Value = "Experiment" + test.SelectedNode.Parent.Value;
                ProjectMenu.Items[3].Value = "Computation" + test.SelectedNode.Parent.Value;
                ProjectMenu.Items[4].Value = "Signal" + test.SelectedNode.Parent.Value;

            }
            else if (test.SelectedNode.Text.Equals("Computation Level"))
            {

                DataBindDownloadView();
                MovetoTheNextPanelState(null, "LoadData_General");
                
                PrTitleLabel.Text = "Current project: ";
                PrTitleLabel.Text += test.SelectedNode.Parent.Text;
                PrIDLabel.Text = test.SelectedNode.Parent.Value;
       
                
                MultiView1.ActiveViewIndex = 7;
                DataBindGridView(GridCompExpLevelPerson, H4ExpCompPerson, "Computation Personnel ");
                DataBindGridView(GridCompExpLevelData, H4ExpCompData, "Computation data");
                DataBindGridView(GridCompExpLevelDLC, H4ExpCompDLC, "Computation Detailed Loading Characteristics");
                DataBindGridView(GridCompExpLevelDOC, H4ExpCompDoc, "Computation Documents");
                DataBindGridView(GridCompExpLevelIMG, H4ExpCompIMGS, "Computation Imaegs");
                DataBindGridView(GridCompExpLevelMeshModel, H4ExpCompMeshModel, "Mesh model");
                DataBindGridView(GridCompExpLevelMeshModelDoc, H4ExpCompMeshModelDoc, "Mesh Model Documents");
                DataBindGridView(GridCompExpLevelMeshModelIMG, H4ExpCompMeshModelIMG, "Mesh Model Images");
                DataBindGridView(GridCompExpLevelOLS, H4ExpCompOLS, "Original Loading Signals");
                DataBindGridView(GridCompExpLevelPC,H4ExpCompPC,"Computer System") ;
                DataBindGridView(GridCompExpLevelVideo,H4ExpCompVideo,"Computation Video");
                DataBindGridView(GridCompExpLevelInputFile, H4ExpCompInputFile, "Effective Input Files");
                DataBindGridView(GridCompExpLevelOLS_Signals, H4ExpCompOLS_Signal, "OLS signal Attributes");
                

                FindCompExpLevelView();

                ProjectMenu.Items[3].Selected = true;
                ProjectMenu.Items[0].Value = "Project" + test.SelectedNode.Parent.Value;
                ProjectMenu.Items[1].Value = "Specimen" + test.SelectedNode.Parent.Value;
                ProjectMenu.Items[2].Value = "Experiment" + test.SelectedNode.Parent.Value;
                ProjectMenu.Items[3].Value = "Computation" + test.SelectedNode.Parent.Value;
                ProjectMenu.Items[4].Value = "Signal" + test.SelectedNode.Parent.Value;

            }
            else if (test.SelectedNode.Text.Equals("Signal Level"))
            {

                DataBindDownloadView();
                
                MovetoTheNextPanelState(null, "LoadData_General");
                PrTitleLabel.Text = "Current project: ";
                
                PrTitleLabel.Text += test.SelectedNode.Parent.Text;
                PrIDLabel.Text = test.SelectedNode.Parent.Value;


                MultiView1.ActiveViewIndex = 8;
                DataBindGridView(GridSignalLevel, H4SignalData, "Signal Data");

                FindSignalLevelView();

                ProjectMenu.Items[4].Selected = true;
                ProjectMenu.Items[0].Value = "Project" + test.SelectedNode.Parent.Value;
                ProjectMenu.Items[1].Value = "Specimen" + test.SelectedNode.Parent.Value;
                ProjectMenu.Items[2].Value = "Experiment" + test.SelectedNode.Parent.Value;
                ProjectMenu.Items[3].Value = "Computation" + test.SelectedNode.Parent.Value;
                ProjectMenu.Items[4].Value = "Signal" + test.SelectedNode.Parent.Value;
               
            }
            else if (test.SelectedNode.Value.Contains("Project"))
            {

                DataBindDownloadView();

                MovetoTheNextPanelState(null, "LoadData_General");

                PrTitleLabel.Text = "Current project: ";
                PrTitleLabel.Text += test.SelectedNode.Text;
                PrIDLabel.Text = test.SelectedNode.Value;

                string postbackControlName = Page.Request.Params.Get("__EVENTTARGET");
                MultiView1.ActiveViewIndex = 2;

                DataBindGridView(GrdViewDetPrData,H4PrData, "Project Data");
                DataBindGridView(GrdViewDetPrInvest,H4PrInvestigator,"Project Investigators");
                DataBindGridView(GrdViewDetPrInfrastr, H4PrInfrstr,"Project Infrastructure");
                DataBindGridView(GrdViewDetPrDoc,H4PrDoc,"Project Documents");
                
                FindProjectLevelView();

                ProjectMenu.Items[0].Selected = true;
                ProjectMenu.Items[0].Value = "Project" + test.SelectedNode.Value;
                ProjectMenu.Items[1].Value = "Specimen" + test.SelectedNode.Value;
                ProjectMenu.Items[2].Value = "Experiment" + test.SelectedNode.Value;
                ProjectMenu.Items[3].Value = "Computation" + test.SelectedNode.Value;
                ProjectMenu.Items[4].Value = "Signal" + test.SelectedNode.Value;


                //DataBindGridView(GridView19,H4PrMainFocus ,"Project Main Focus");
                //DataBindGridView(GridView20,H4PrExpType,"Project Experiment Type");
                
                // BreadCrumb.InnerHtml+= test.SelectedNode.Parent.Text+ "->" + test.SelectedNode.Text;

            }
            else if (test.SelectedNode.Value.Contains("Specimen"))
            {


                DataBindDownloadView();

                MovetoTheNextPanelState(null, "LoadData_General");

                ProjectMenu.Items[0].Value = "Project" + test.SelectedNode.Parent.Parent.Value;
                ProjectMenu.Items[1].Value = "Specimen" + test.SelectedNode.Parent.Parent.Value;
                ProjectMenu.Items[2].Value = "Experiment" + test.SelectedNode.Parent.Parent.Value;
                ProjectMenu.Items[3].Value = "Computation" + test.SelectedNode.Parent.Parent.Value;
                ProjectMenu.Items[4].Value = "Signal" + test.SelectedNode.Parent.Parent.Value;


                PrTitleLabel.Text = "Current project: ";
                PrTitleLabel.Text += test.SelectedNode.Parent.Parent.Text;
                PrIDLabel.Text = test.SelectedNode.Parent.Parent.Value;

                if (test.SelectedNode.Parent.Text.Equals("Specimen Level"))
                {
                    ProjectMenu.Items[1].Selected = true;
                    MultiView1.ActiveViewIndex = 6;
          
                    DataBindGridView(GridSpecLevelSpecData, H4SpecData, "Specimen Data");
                    DataBindGridView(GridSpecLevelSpecDocs, H4SpecDoc, "Specimen Document");
                    DataBindGridView(GridSpecLevelSpecIMGs, H4SpecImg, "Specimen Images");
                    DataBindGridView(GridSpecLevelSpecScaling, H4SpecScaling, "Specimen Scaling");
                    DataBindGridView(GridSpecLevelStrElemData, H4SpecStrElem, "Structural Element Data");
                    DataBindGridView(GridSpecLevelStrElemMatActProp, H4StrlElem_Mat_ActualProp, "Structual Element and Material Actual Properties");
                    DataBindGridView(GridSpecLevelStrElemMaterial, H4SpecStrElemMat, "Structural Element and it's Material");
                    DataBindGridView(GridSpecLevelStrElemMatNomProp, H4StrlElem_Mat_Nom_Prop, "Structural Elements and material Nominal Properties");

                    FindSpecimenLevelView();
              
                }
                else if (test.SelectedNode.Parent.Text.Equals("Experiment Level") )
                {
                    ProjectMenu.Items[2].Selected = true;
                    
                    MultiView1.ActiveViewIndex = 7;
                    DataBindGridView(GridCompExpLevelPerson, H4ExpCompPerson, "Experiment Personnel ");
                    DataBindGridView(GridCompExpLevelData, H4ExpCompData, "Experiment data");
                    DataBindGridView(GridCompExpLevelDLC, H4ExpCompDLC, "Experiment Detailed Loading Characteristics");
                    DataBindGridView(GridCompExpLevelDOC, H4ExpCompDoc, "Experiment Documents");
                    DataBindGridView(GridCompExpLevelIMG, H4ExpCompIMGS, "Experimen Imaegs");
                    DataBindGridView(GridCompExpLevelMeshModel, H4ExpCompMeshModel, "Mesh model");
                    DataBindGridView(GridCompExpLevelMeshModelDoc, H4ExpCompMeshModelDoc, "Mesh Model Documents");
                    DataBindGridView(GridCompExpLevelMeshModelIMG, H4ExpCompMeshModelIMG, "Mesh Model Images");
                    DataBindGridView(GridCompExpLevelOLS, H4ExpCompOLS, "Original Loading Signals");
                    DataBindGridView(GridCompExpLevelPerson, H4ExpCompPerson, "Experiment Imvestigators");
                    DataBindGridView(GridCompExpLevelVideo, H4ExpCompVideo, "Experiment Video");
                    DataBindGridView(GridCompExpLevelPC, H4ExpCompPC, "Computer System");
                    DataBindGridView(GridCompExpLevelInputFile, H4ExpCompInputFile, "Effective Input Files");
                    DataBindGridView(GridCompExpLevelOLS_Signals, H4ExpCompOLS_Signal, "OLS signal Attributes");
                
                    FindCompExpLevelView();
                
                }
                else if (test.SelectedNode.Parent.Text.Equals("Computation Level"))
                {
                    DataBindDownloadView();

                    ProjectMenu.Items[3].Selected = true;
                    
                    MultiView1.ActiveViewIndex = 7;
                    DataBindGridView(GridCompExpLevelPerson, H4ExpCompPerson, "Computation Personnel");
                    DataBindGridView(GridCompExpLevelData, H4ExpCompData, "Computation data");
                    DataBindGridView(GridCompExpLevelDLC, H4ExpCompDLC, "Computation Detailed Loading Characteristics");
                    DataBindGridView(GridCompExpLevelDOC, H4ExpCompDoc, "Computation Documents");
                    DataBindGridView(GridCompExpLevelIMG, H4ExpCompIMGS, "Computation Imaegs");
                    DataBindGridView(GridCompExpLevelMeshModel, H4ExpCompMeshModel, "Mesh model");
                    DataBindGridView(GridCompExpLevelMeshModelDoc, H4ExpCompMeshModelDoc, "Mesh Model Documents");
                    DataBindGridView(GridCompExpLevelMeshModelIMG, H4ExpCompMeshModelIMG, "Mesh Model Images");
                    DataBindGridView(GridCompExpLevelOLS, H4ExpCompOLS, "Original Loading Signals");
                    DataBindGridView(GridCompExpLevelPC, H4ExpCompPC, "Computer System");
                    DataBindGridView(GridCompExpLevelVideo, H4ExpCompVideo, "Computation Video");
                    DataBindGridView(GridCompExpLevelInputFile, H4ExpCompInputFile, "Effective Input Files");
                    DataBindGridView(GridCompExpLevelOLS_Signals, H4ExpCompOLS_Signal, "OLS signal Attributes");
                

                    FindCompExpLevelView();

                }
                else if (test.SelectedNode.Parent.Text.Equals("Signal Level"))
                {
                    ProjectMenu.Items[4].Selected = true;
                    
                    MultiView1.ActiveViewIndex = 8;
                    DataBindGridView(GridSignalLevel, H4SignalData, "Signal Data");

                    FindSignalLevelView();


                }
                //BreadCrumb.InnerHtml += test.SelectedNode.Parent.Parent.Text + "->" + test.SelectedNode.Parent.Text + "->" + test.SelectedNode.Text;


            }
            else if (test.SelectedNode.Value.Contains("CompExp"))
            {
                DataBindDownloadView();

                MovetoTheNextPanelState(null, "LoadData_General");

                ProjectMenu.Items[0].Value = "Project" + test.SelectedNode.Parent.Parent.Parent.Value;
                ProjectMenu.Items[1].Value = "Specimen" + test.SelectedNode.Parent.Parent.Parent.Value;
                ProjectMenu.Items[2].Value = "Experiment" + test.SelectedNode.Parent.Parent.Parent.Value;
                ProjectMenu.Items[3].Value = "Computation" + test.SelectedNode.Parent.Parent.Parent.Value;
                ProjectMenu.Items[4].Value = "Signal" + test.SelectedNode.Parent.Parent.Parent.Value;

                PrTitleLabel.Text = "Current project: ";
                PrTitleLabel.Text += test.SelectedNode.Parent.Parent.Parent.Text;
                PrIDLabel.Text = test.SelectedNode.Parent.Parent.Parent.Text;

                if (test.SelectedNode.Parent.Parent.Text.Equals("Experiment Level"))
                {
                    ProjectMenu.Items[2].Selected = true;
                    
                    MultiView1.ActiveViewIndex = 7;
                    DataBindGridView(GridCompExpLevelPerson, H4ExpCompPerson, "Experiment Personnel");
                    DataBindGridView(GridCompExpLevelData, H4ExpCompData, "Experiment data");
                    DataBindGridView(GridCompExpLevelDLC, H4ExpCompDLC, "Experiment Detailed Loading Characteristics");
                    DataBindGridView(GridCompExpLevelDOC, H4ExpCompDoc, "Experiment Documents");
                    DataBindGridView(GridCompExpLevelIMG, H4ExpCompIMGS, "Experimen Imaegs");
                    DataBindGridView(GridCompExpLevelMeshModel, H4ExpCompMeshModel, "Mesh model");
                    DataBindGridView(GridCompExpLevelMeshModelDoc, H4ExpCompMeshModelDoc, "Mesh Model Documents");
                    DataBindGridView(GridCompExpLevelMeshModelIMG, H4ExpCompMeshModelIMG, "Mesh Model Images");
                    DataBindGridView(GridCompExpLevelOLS, H4ExpCompOLS, "Original Loading Signals");
                    DataBindGridView(GridCompExpLevelPerson, H4ExpCompPerson, "Experiment Imvestigators");
                    DataBindGridView(GridCompExpLevelVideo, H4ExpCompVideo, "Experiment Video");
                    DataBindGridView(GridCompExpLevelPC, H4ExpCompPC, "Computer System");
                    DataBindGridView(GridCompExpLevelInputFile, H4ExpCompInputFile, "Effective Input Files");
                    DataBindGridView(GridCompExpLevelOLS_Signals, H4ExpCompOLS_Signal, "OLS signal Attributes");
                

                    FindCompExpLevelView();
                
                }
                else if (test.SelectedNode.Parent.Parent.Text.Equals("Computation Level"))
                {

                  
                    ProjectMenu.Items[3].Selected = true;

                    
                    MultiView1.ActiveViewIndex = 7;
                    DataBindGridView(GridCompExpLevelPerson, H4ExpCompPerson, "Computation Personnel ");
                    DataBindGridView(GridCompExpLevelData, H4ExpCompData, "Computation data");
                    DataBindGridView(GridCompExpLevelDLC, H4ExpCompDLC, "Computation Detailed Loading Characteristics");
                    DataBindGridView(GridCompExpLevelDOC, H4ExpCompDoc, "Computation Documents");
                    DataBindGridView(GridCompExpLevelIMG, H4ExpCompIMGS, "Computation Imaegs");
                    DataBindGridView(GridCompExpLevelMeshModel, H4ExpCompMeshModel, "Mesh model");
                    DataBindGridView(GridCompExpLevelMeshModelDoc, H4ExpCompMeshModelDoc, "Mesh Model Documents");
                    DataBindGridView(GridCompExpLevelMeshModelIMG, H4ExpCompMeshModelIMG, "Mesh Model Images");
                    DataBindGridView(GridCompExpLevelOLS, H4ExpCompOLS, "Original Loading Signals");
                    DataBindGridView(GridCompExpLevelPC, H4ExpCompPC, "CComputer System");
                    DataBindGridView(GridCompExpLevelVideo, H4ExpCompVideo, "Computation Video");
                    DataBindGridView(GridCompExpLevelInputFile, H4ExpCompInputFile, "Effective Input Files");
                    DataBindGridView(GridCompExpLevelOLS_Signals, H4ExpCompOLS_Signal, "OLS signal Attributes");
                

                    FindCompExpLevelView();

                }
                else if (test.SelectedNode.Parent.Parent.Text.Equals("Signal Level"))
                {
                    ProjectMenu.Items[4].Selected = true;
                   
                    MultiView1.ActiveViewIndex = 8;
                    DataBindGridView(GridSignalLevel, H4SignalData, "Signal Data");
                    FindSignalLevelView();
                }

            }
            else if (test.SelectedNode.Value.Contains("Signal"))
            {
                DataBindDownloadView();
                PrTitleLabel.Text = "Current project: ";
                PrTitleLabel.Text += test.SelectedNode.Parent.Parent.Parent.Text;
                PrIDLabel.Text = test.SelectedNode.Parent.Parent.Parent.Text;

                MovetoTheNextPanelState(null, "LoadData_General");

                MultiView1.ActiveViewIndex = 8;
                DataBindGridView(GridSignalLevel, H4SignalData, "Signal Data");
                    //BreadCrumb.InnerHtml += test.SelectedNode.Parent.Parent.Parent.Parent.Text + "->" + test.SelectedNode.Parent.Parent.Parent.Text + "->" + test.SelectedNode.Parent.Parent.Text + "->" + test.SelectedNode.Parent.Text + "->" + test.SelectedNode.Text;

                FindSignalLevelView();

                ProjectMenu.Items[0].Value = "Project" + test.SelectedNode.Parent.Parent.Parent.Value;
                ProjectMenu.Items[1].Value = "Specimen" + test.SelectedNode.Parent.Parent.Parent.Value;
                ProjectMenu.Items[2].Value = "Experiment" + test.SelectedNode.Parent.Parent.Parent.Value;
                ProjectMenu.Items[3].Value = "Computation" + test.SelectedNode.Parent.Parent.Parent.Value;
                ProjectMenu.Items[4].Value = "Signal" + test.SelectedNode.Parent.Parent.Parent.Value;
            }
            else if (test.SelectedNode.Value.Contains("Lab"))
            {  
                PrTitleLabel.Text = null;
                PrIDLabel.Text = null;

                MovetoTheNextPanelState(null, "LoadData_Lab");
                GridLabInfo.DataBind();
                GridLabInfo.Visible = true;

            }
            else if (test.SelectedNode.Value == "RootNode")
            {
                MovetoTheNextPanelState(null, "RootNode");
            }
            else if (test.SelectedNode.Value == "Partner root")
            {
                MovetoTheNextPanelState(null, "RootNode");
            }


            if (PrTitleLabel.Text!="")
                ProjectMenu.Items[0].Text = PrTitleLabel.Text.Remove(0,17);



        }

        protected void FindDownloadView()
        {
            if (DownloadExpDoc_GrdV.Rows.Count == 0 && DownloadExpImg_GrdV.Rows.Count == 0 && DownloadExpVid_GrdV.Rows.Count == 0
           && DownloadMeshModelDoc_GrdV.Rows.Count == 0
            && DownloadMeshModelImg_GrdV.Rows.Count == 0
            && DownloadPrDoc_GrdV.Rows.Count == 0
           && DownloadSignalResult_GrdV.Rows.Count == 0
           && DownloadSpecDoc_GrdV.Rows.Count == 0
           && DownloadSpecImg_GrdV.Rows.Count == 0
           )
                DownloadMultiView.ActiveViewIndex = 1;
            else
                DownloadMultiView.ActiveViewIndex = 0;
        }
        protected void PopulateNodes(object sender, System.Web.UI.WebControls.TreeNodeEventArgs e)
        {
            if (e.Node.ChildNodes.Count == 0 )
            {
                string orderbyvalue =GetDropDownValue();
                if (orderbyvalue.Equals("Lab") || orderbyvalue.Equals("OrderBy"))
                    FillLabs("public", e.Node);
                else
                    FillTree("PUBLIC", orderbyvalue, TreeView2.FindNode("RootNode"));

            }

        }
        protected void FillLabs(string privacy, TreeNode node)
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;User Id=root;database=newseriesserver;password=root;");
            MySqlCommand selectLabs = new MySqlCommand("Select * from laboratory", conn);

            MySqlDataAdapter labsAdapter = new MySqlDataAdapter(selectLabs);
            System.Data.DataSet labs = new System.Data.DataSet();
            labsAdapter.Fill(labs);
            if (labs.Tables.Count > 0)
            {
                foreach (System.Data.DataRow Labrow in labs.Tables[0].Rows)
                {

                    string LabID = Labrow["idlaboratory"].ToString();
                   // string Text = Labrow["Name"].ToString() + " (Last Update: " + Labrow["LastUpdateTime"] + " )";
                    string Text = Labrow["LongName"].ToString();
                    
                    node.ChildNodes.Add(FillProject(Text, LabID,privacy));

                }
               // node.ImageUrl = "newcss/BreadCrumHOME.jpg";
            }

        }
        protected TreeNode FillProject(string LabText, string LabID,string privacy)
        {

            string Value = "LabID:" + LabID;
            MySqlConnection conn = new MySqlConnection("server=localhost;User Id=root;database=newseriesserver;password=root;");
            MySqlCommand selectProjects = new MySqlCommand("select * from Project where laboratory_idlaboratory =  @LabID and privacy = @privacy", conn);
            selectProjects.Parameters.AddWithValue("@LabID", LabID);
            selectProjects.Parameters.AddWithValue("@privacy", privacy);
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectProjects);
            System.Data.DataSet projects = new System.Data.DataSet();
            adapter.Fill(projects);

            TreeNode LabNode = new TreeNode();
            

            LabNode.Text = LabText;
            LabNode.Value = Value;
            
            if (projects.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in projects.Tables[0].Rows)
                {
                    TreeNode ProjectNode = new TreeNode();
                    ProjectNode.ImageUrl = "newcss/Folder.gif";
                    
                    TreeNode SpecimenLevelNode = new TreeNode();
                    SpecimenLevelNode.ImageUrl = "newcss/Folder.gif";
                    TreeNode ExpLevelNode = new TreeNode();
                    ExpLevelNode.ImageUrl = "newcss/Folder.gif";
                    TreeNode CompLevelNode = new TreeNode();
                    CompLevelNode.ImageUrl = "newcss/Folder.gif";
                    TreeNode SignalLevelNode = new TreeNode();
                    SignalLevelNode.ImageUrl = "newcss/Folder.gif";


                    SpecimenLevelNode.Text="Specimen Level";
                    SpecimenLevelNode.Value="SpecimenLevelID:" + row["idproject"].ToString();
                    ExpLevelNode.Text = "Experiment Level";
                    ExpLevelNode.Value = "ExpLevelID:" + row["idproject"].ToString();
                    CompLevelNode.Text = "Computation Level";
                    CompLevelNode.Value = "CompLevelID:" + row["idproject"].ToString();
                    SignalLevelNode.Text = "Signal Level";
                    SignalLevelNode.Value = "SignalLevelID:" + row["idproject"].ToString();
                    ProjectNode.ChildNodes.Add(SpecimenLevelNode);
                    ProjectNode.ChildNodes.Add(ExpLevelNode);
                    ProjectNode.ChildNodes.Add(CompLevelNode);
                    ProjectNode.ChildNodes.Add(SignalLevelNode);
            
                    string ProjectText = row["Title"].ToString();
                    string ProjectID = row["idproject"].ToString();
                    ProjectNode.Text = ProjectText;
                    ProjectNode.Value = "ProjectID:" + ProjectID;
                    


                    LabNode.ChildNodes.Add(ProjectNode);
                    //ProjectNode.ChildNodes.Add(FillSpecimens(ProjectID, ProjectText));
                    FillSpecimens(SpecimenLevelNode,"Specimen Level");
                    FillSpecimens(ExpLevelNode, "Experiment Level");
                    FillSpecimens(CompLevelNode, "Computation Level");
                    FillSpecimens(SignalLevelNode, "Signal Level");

                    ProjectNode.Collapse();
                    SpecimenLevelNode.Collapse();
                    ExpLevelNode.Collapse();
                    CompLevelNode.Collapse();
                    SignalLevelNode.Collapse();

                }
            }
            LabNode.Collapse();
            return LabNode;
        }
        protected void FillTree(string privacy,string orderby,TreeNode TreeNodeTobeChanged)
        {


            
           MySqlConnection conn = new MySqlConnection("server=localhost;User Id=root;database=newseriesserver;password=root;");
           MySqlCommand selectProjects = new MySqlCommand("select * from Project where privacy =  @privacy ORDER BY  " + orderby, conn);
            selectProjects.Parameters.AddWithValue("@privacy", privacy);
            //selectProjects.Parameters.AddWithValue("@orderby", orderby);
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectProjects);
            System.Data.DataSet projects = new System.Data.DataSet();
            adapter.Fill(projects);


            if (projects.Tables.Count > 0)
            {
                foreach (System.Data.DataRow row in projects.Tables[0].Rows)
                {
                    TreeNode ProjectNode = new TreeNode();
                    ProjectNode.ImageUrl = "newcss/Folder.gif";

                    TreeNode SpecimenLevelNode = new TreeNode();
                    SpecimenLevelNode.ImageUrl = "newcss/Folder.gif";
                    TreeNode ExpLevelNode = new TreeNode();
                    ExpLevelNode.ImageUrl = "newcss/Folder.gif";
                    TreeNode CompLevelNode = new TreeNode();
                    CompLevelNode.ImageUrl = "newcss/Folder.gif";
                    TreeNode SignalLevelNode = new TreeNode();
                    SignalLevelNode.ImageUrl = "newcss/Folder.gif";


                    SpecimenLevelNode.Text = "Specimen Level";
                    SpecimenLevelNode.Value = "SpecimenLevelID:" + row["idproject"].ToString();
                    ExpLevelNode.Text = "Experiment Level";
                    ExpLevelNode.Value = "ExpLevelID:" + row["idproject"].ToString();
                    CompLevelNode.Text = "Computation Level";
                    CompLevelNode.Value = "CompLevelID:" + row["idproject"].ToString();
                    SignalLevelNode.Text = "Signal Level";
                    SignalLevelNode.Value = "SignalLevelID:" + row["idproject"].ToString();
                    ProjectNode.ChildNodes.Add(SpecimenLevelNode);
                    ProjectNode.ChildNodes.Add(ExpLevelNode);
                    ProjectNode.ChildNodes.Add(CompLevelNode);
                    ProjectNode.ChildNodes.Add(SignalLevelNode);

                    string ProjectText = row["Title"].ToString();
                    string ProjectID = row["idproject"].ToString();
                    ProjectNode.Text = ProjectText;
                    ProjectNode.Value = "ProjectID:" + ProjectID;


                    TreeNodeTobeChanged.ChildNodes.Add(ProjectNode);
                

                   
                    //ProjectNode.ChildNodes.Add(FillSpecimens(ProjectID, ProjectText));
                    FillSpecimens(SpecimenLevelNode, "Specimen Level");
                    FillSpecimens(ExpLevelNode, "Experiment Level");
                    FillSpecimens(CompLevelNode, "Computation Level");
                    FillSpecimens(SignalLevelNode, "Signal Level");

                    ProjectNode.Collapse();
                    SpecimenLevelNode.Collapse();
                    ExpLevelNode.Collapse();
                    CompLevelNode.Collapse();
                    SignalLevelNode.Collapse();

                }
            }
            TreeView2.Visible = true;
            conn.Close();
        }
        protected void AddProjectToTree( string idproject,string Prtitle, TreeNode TreeNodeTobeChanged)
        {


            

                TreeNode ProjectNode = new TreeNode();
                    ProjectNode.ImageUrl = "newcss/Folder.gif";

                    TreeNode SpecimenLevelNode = new TreeNode();
                    SpecimenLevelNode.ImageUrl = "newcss/Folder.gif";
                    TreeNode ExpLevelNode = new TreeNode();
                    ExpLevelNode.ImageUrl = "newcss/Folder.gif";
                    TreeNode CompLevelNode = new TreeNode();
                    CompLevelNode.ImageUrl = "newcss/Folder.gif";
                    TreeNode SignalLevelNode = new TreeNode();
                    SignalLevelNode.ImageUrl = "newcss/Folder.gif";


                    SpecimenLevelNode.Text = "Specimen Level";
                    SpecimenLevelNode.Value = "SpecimenLevelID:" + idproject;
                    ExpLevelNode.Text = "Experiment Level";
                    ExpLevelNode.Value = "ExpLevelID:" + idproject;
                    CompLevelNode.Text = "Computation Level";
                    CompLevelNode.Value = "CompLevelID:" + idproject;
                    SignalLevelNode.Text = "Signal Level";
                    SignalLevelNode.Value = "SignalLevelID:" + idproject;
                    ProjectNode.ChildNodes.Add(SpecimenLevelNode);
                    ProjectNode.ChildNodes.Add(ExpLevelNode);
                    ProjectNode.ChildNodes.Add(CompLevelNode);
                    ProjectNode.ChildNodes.Add(SignalLevelNode);

                    string ProjectText = Prtitle;
                    string ProjectID = idproject;
                    ProjectNode.Text = ProjectText;
                    ProjectNode.Value = "ProjectID:" + ProjectID;


                    TreeNodeTobeChanged.ChildNodes.Add(ProjectNode);



                    //ProjectNode.ChildNodes.Add(FillSpecimens(ProjectID, ProjectText));
                    FillSpecimens(SpecimenLevelNode, "Specimen Level");
                    FillSpecimens(ExpLevelNode, "Experiment Level");
                    FillSpecimens(CompLevelNode, "Computation Level");
                    FillSpecimens(SignalLevelNode, "Signal Level");

                    ProjectNode.Collapse();
                    SpecimenLevelNode.Collapse();
                    ExpLevelNode.Collapse();
                    CompLevelNode.Collapse();
                    SignalLevelNode.Collapse();

           TreeView2.Visible = true;
            conn.Close();
        }
      
             
        protected TreeNode FillSpecimens(TreeNode LevelNode, string Level)
        {
            TreeNode TempNode = new TreeNode();
            //SpecimenLevel.ImageUrl = "images/project.gif";
            string SpecimenID;
            string[] id = LevelNode.Value.Split(':'); 
            MySqlConnection conn = new MySqlConnection("server=localhost;User Id=root;database=newseriesserver;password=root;");
            MySqlCommand selectSpecimen = new MySqlCommand("select * from Specimen where Project_idProject = @idproject", conn);
            selectSpecimen.Parameters.AddWithValue("@idproject", id[1]);
            MySqlDataAdapter adapter2 = new MySqlDataAdapter(selectSpecimen);
            System.Data.DataSet specimen = new System.Data.DataSet();
            adapter2.Fill(specimen);
            if (specimen.Tables.Count > 0)
            {
                foreach (System.Data.DataRow spec in specimen.Tables[0].Rows)
                {
                    SpecimenID = spec[0].ToString();
                    string SpecimenText = spec["Name"].ToString();
                    TempNode = FillExpComp(SpecimenID, SpecimenText, Level);
                    if(!Level.Equals("Specimen Level")&&(TempNode.ChildNodes.Count !=0))
                        LevelNode.ChildNodes.Add(TempNode);
                    else if(Level.Equals("Specimen Level"))
                        LevelNode.ChildNodes.Add(TempNode);
                }
            }
            
            return LevelNode;

        }
        protected TreeNode FillSpecimens(TreeNode LevelNode, string Level,string ColorId)
        {
            TreeNode TempNode = new TreeNode();
           
            //SpecimenLevel.ImageUrl = "images/project.gif";
            string SpecimenID;
            string[] id = LevelNode.Value.Split(':');
            MySqlConnection conn = new MySqlConnection("server=localhost;User Id=root;database=newseriesserver;password=root;");
            MySqlCommand selectSpecimen = new MySqlCommand("select * from Specimen where Project_idProject = @idproject", conn);
            selectSpecimen.Parameters.AddWithValue("@idproject", id[1]);
            MySqlDataAdapter adapter2 = new MySqlDataAdapter(selectSpecimen);
            System.Data.DataSet specimen = new System.Data.DataSet();
            adapter2.Fill(specimen);
            if (specimen.Tables.Count > 0)
            {
                foreach (System.Data.DataRow spec in specimen.Tables[0].Rows)
                {
                    SpecimenID = spec[0].ToString();
                    string SpecimenText = spec["Name"].ToString();
                    TempNode = FillExpComp(SpecimenID, SpecimenText, Level);
                    if (!Level.Equals("Specimen Level") && (TempNode.ChildNodes.Count != 0))
                        LevelNode.ChildNodes.Add(TempNode);
                    else if (Level.Equals("Specimen Level"))
                    {
                        if (ColorId.Equals(SpecimenID))
                            TempNode.Text = "<font style='color:#A52A2A;'>" + TempNode.Text + "</font>";
                        LevelNode.ChildNodes.Add(TempNode);
                    }
                }
            }

            LevelNode.Text = "<font style='color:#A52A2A;'>" + LevelNode.Text + "</font>";
     
            return LevelNode;

        }
        protected TreeNode FillExpComp(string SpecimenID, string SpecimenText,string Level)
        {
            TreeNode SpecimenNode = new TreeNode(SpecimenText);
            TreeNode TempNode = new TreeNode();
            string Type = null;


            MySqlConnection conn = new MySqlConnection("server=localhost;User Id=root;database=newseriesserver;password=root;");
            MySqlCommand SelectCompExp = null;
            if (Level.Contains("Computation"))
            {
                Type = "Computation";
                SelectCompExp = new MySqlCommand("Select * from CompExp Where Specimen_idSpecimen = @idSpecimen AND type = @type", conn);
            }
            else if (Level.Contains("Experiment"))
            {
                Type = "Experiment";
                SelectCompExp = new MySqlCommand("Select * from CompExp Where Specimen_idSpecimen = @idSpecimen AND type = @type", conn);
            }
            else
            {
                Type = null;
                SelectCompExp = new MySqlCommand("Select * from CompExp Where Specimen_idSpecimen = @idSpecimen", conn);
            }
            
            SpecimenNode.Value = "SpecimenID:" + SpecimenID;
            SpecimenNode.ImageUrl = "images/specimen.gif";


            
            SelectCompExp.Parameters.AddWithValue("@idSpecimen", SpecimenID);
            SelectCompExp.Parameters.AddWithValue("@type", Type);
            MySqlDataAdapter adapter3 = new MySqlDataAdapter(SelectCompExp);
            System.Data.DataSet CompExp = new System.Data.DataSet();
            adapter3.Fill(CompExp);
            if (CompExp.Tables.Count > 0)
            {
                foreach (System.Data.DataRow Comp in CompExp.Tables[0].Rows)
                {
                    string CompExpText = Comp["Name"].ToString();
                    string CompExpID = Comp["idCompExp"].ToString();
                    if (!Level.Equals("Specimen Level"))
                    {
                        TempNode = FillSignals(CompExpID, CompExpText, Level);
                        if ((Level.Equals("Signal Level") && (TempNode.ChildNodes.Count != 0)) || (Level.Equals("Experiment Level"))||(Level.Equals("Computation Level")))
                            SpecimenNode.ChildNodes.Add(TempNode);                    
                    }
                }
            }
       
            return SpecimenNode;
        }
        protected TreeNode FillSignals(string CompExpID, string CompExpText,string Level)
        {
            TreeNode CompExpNode = new TreeNode(CompExpText);
            CompExpNode.Value = "CompExpID:" + CompExpID;
            CompExpNode.ImageUrl = "images/compexp.gif";
            if(Level.Equals("Signal Level"))
            {

                MySqlConnection conn4 = new MySqlConnection("server=localhost;User Id=root;database=newseriesserver;password=root;");
                MySqlCommand SelectSignals = new MySqlCommand("Select * from signalresult where Compexp_idcompexp = @compexpid", conn4);
                SelectSignals.Parameters.AddWithValue("@compexpid", CompExpID);
                MySqlDataAdapter adapter4 = new MySqlDataAdapter(SelectSignals);
                System.Data.DataSet Signal = new System.Data.DataSet();
                adapter4.Fill(Signal);
                if (Signal.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow SignalRow in Signal.Tables[0].Rows)
                    {
                        TreeNode SignalNode = new TreeNode();
                        SignalNode.Text = SignalRow["SignaLabel"].ToString();
                        SignalNode.ImageUrl = "images/signal.gif";
                        SignalNode.Value = "SignalID:" + SignalRow["idSignalResult"].ToString();
                        string SignalID = SignalRow["idSignalResult"].ToString();
                        CompExpNode.ChildNodes.Add(SignalNode);
                    }
                }
            }

            return CompExpNode;
            
        }
       
        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            NumPrRes = 0;
            NumSpecRes = 0;
            NumCompExpRes = 0;
            NumSigRes = 0;
            MovetoTheNextPanelState(null, "Search");
           
            Session["LeftState"] =  MoveToTheNextState("search", Session["LeftState"].ToString());
            


            GoogleDivs.InnerHtml = "";

            string ValuePathSeparator = "<b>Search Criteria:</b><br/>";
            //make it visible
            SearchTree.Visible = true;
            //if this is not first time search tree has results so it has to be cleared
            SearchTree.FindNode("Search Results").ChildNodes.Clear();

            


            

            if (ListBox_Location.SelectedIndex != -1)
            {
                ValuePathSeparator += "<b>Location:</b>" + FindSearchProjectIDs(ListBox_Location, "Location") + "<br/>";
            }
            if (ListBox_Infrastructure.SelectedIndex != -1)
            {
                ValuePathSeparator += "<b>Infrastructure:</b>" + FindSearchProjectIDs(ListBox_Infrastructure, "Infrastructure") + "<br/>";
            }

            if (ListBox__ResearchArea.SelectedIndex != -1)
            {
                ValuePathSeparator += "<b>ResearchArea:</b>" + FindSearchProjectIDs(ListBox__ResearchArea, "ResearchArea") + "<br/>";

            }

           
            if (ListBox_PrincipleExpType.SelectedIndex != -1)
            {
                ValuePathSeparator += "<b>PrincipleExperimentType:</b>" + FindSearchProjectIDs(ListBox_PrincipleExpType, "Principle Exp Type") + "<br/>";
            }

            if (ListBox_StructuralElement.SelectedIndex != -1)
            {
                ValuePathSeparator += "<b>StructuralElement:</b>" + FindSearchProjectIDs(ListBox_StructuralElement, "Structural Element") + "<br/>";
            }
           // if (ListBox_Spec_Picture.SelectedIndex != -1)
           // {
            //    ValuePathSeparator += "<b>specimen Pictures:</b>" + FindSearchProjectIDs(ListBox_Spec_Picture, "specimen pictures") + "<br/>";
           // }

            if (ListBox_ExpType.SelectedIndex != -1)
            {
                ValuePathSeparator += "<b>expType:</b>" + FindSearchProjectIDs(ListBox_ExpType, "exptype") + "<br/>";
            }

            if (ListBox_ExpOLS_Nature.SelectedIndex != -1)
            {
                ValuePathSeparator += "<b>OLS Nature:</b>" + FindSearchProjectIDs(ListBox_ExpOLS_Nature, "OLS_NATURE") + "<br/>";
            }

            if (ListBox_ExpOLS_Source.SelectedIndex != -1)
            {
                ValuePathSeparator += "<b>OLS Source:</b>" + FindSearchProjectIDs(ListBox_ExpOLS_Source, "OLS_Source") + "<br/>";
            }

            if (ListBox_CompType.SelectedIndex != -1)
            {
                ValuePathSeparator += "<b>Computation Type:</b>" + FindSearchProjectIDs(ListBox_CompType, "CompType") + "<br/>";
            }

            
            if (ListBox_Signal_PhysicalQ.SelectedIndex != -1)
            {
                ValuePathSeparator += "<b>Physical Q:</b>" + FindSearchProjectIDs(ListBox_Signal_PhysicalQ, "PhysicalQ") + "<br/>";
            }

           
            if (ListBox_Material.SelectedIndex != -1)
            {
                ValuePathSeparator += "<b>Material:</b>" + FindSearchProjectIDs(ListBox_Material, "material") + "<br/>";
            }

            if (ListBox_Acronym.SelectedIndex != -1)
            {
                ValuePathSeparator += "<b>Acronym:</b>" + FindSearchProjectIDs(ListBox_Acronym, "acronym") + "<br/>";
            }

            if (ListBox_Investigator.SelectedIndex != -1)
            {
                ValuePathSeparator += "<b>Investigator:</b>" + FindSearchProjectIDs(ListBox_Investigator, "investigator") + "<br/>";
            }

            if (ListBox_CompSoft.SelectedIndex != -1)
            {
                ValuePathSeparator += "<b>Computation Software:</b>" + FindSearchProjectIDs(ListBox_CompSoft, "CompSoft") + "<br/>";
            }


            Search_Criteria.InnerHtml = "";
            Search_Criteria.InnerHtml = "<br/><br/><br/><br/><br/><br/><b>Results Found: </b>" + NumPrRes + " Results at the project level, " + NumSpecRes + " at the specimen level, " + NumCompExpRes + " at the Computation Experiment level, " + NumSigRes + "at the signal Level.</br>Navigate through the results via the left panel</br></br>";
            Search_Criteria.InnerHtml += ValuePathSeparator;
            //SrchCriteriaBottom.InnerHtml = ValuePathSeparator;

            
            //ListBox_Infrastructure.SelectedIndex = -1;

            //ListBox_Location.SelectedIndex = -1;

            //ListBox__ResearchArea.SelectedIndex = -1;

            //ListBox_PrincipleExpType.SelectedIndex = -1;

            //ListBox_StructuralElement.SelectedIndex = -1;

            //ListBox_Material.SelectedIndex = -1;

            //ListBox_Spec_Picture.SelectedIndex = -1;

            //ListBox_ExpType.SelectedIndex = -1;

            //ListBox_ExpOLS_Nature.SelectedIndex = -1;

            //ListBox_ExpOLS_Source.SelectedIndex = -1;

            //ListBox_CompType.SelectedIndex = -1;

           
            //ListBox_Signal_PhysicalQ.SelectedIndex = -1;

            //ListBox_Investigator.SelectedIndex = -1;

            //ListBox_Acronym.SelectedIndex = -1;

            //ListBox_CompSoft.SelectedIndex = -1;
            h4SearchPr.InnerHtml="<a class=\"collapsed\">&nbsp; &nbsp; &nbsp;  Project</a>";
            DivSearchPr.Style.Add("display", "none");
            h4SearchSpec.InnerHtml = "<a class=\"collapsed\">&nbsp; &nbsp; &nbsp;  specimen</a>";
            DivSearchSpec.Style.Add("display", "none");
            h4SearchCompExp.InnerHtml="<a class=\"collapsed\">&nbsp; &nbsp; &nbsp;  Comp/Exp</a>";
            DivSearchCompExp.Style.Add("display", "none");
            h4SearchSignal.InnerHtml="<a class=\"collapsed\">&nbsp; &nbsp; &nbsp;  Signal</a>";
            DivSearchSignal.Style.Add("display", "none");
            
            
            

        }
        protected void BtnFreeSearch(object sender, EventArgs e) 
        {
             string[] KeyWords =null;
            string clickedBtn = ((ImageButton)sender).ID;
            if(clickedBtn.Equals("BtnFreeSearch"))
                KeyWords = Txt_FreeSearch.Value.Split(' ');
            else
                KeyWords = lblTxtUp.Value.Split(' ');
            
            MovetoTheNextPanelState(null, "Search");
            SearchTree.FindNode("Search Results").ChildNodes.Clear();
            Session["LeftState"] = MoveToTheNextState("search", Session["LeftState"].ToString());
            GoogleDivs.InnerHtml = "";
            PrIdsList.Add(Initial);


            
            string MySqlCmdFacility = "select projectid, projectTitle, abstract, startdate,enddate,labname,labid,specimenid,compexpid,signalid from search where  ";

            for (int i = 0; i < KeyWords.Length -1;i++ )
            {
                MySqlCmdFacility += " search.keyword = \"" + KeyWords[i] + "\" OR";
            }

            MySqlCmdFacility += " search.keyword = \"" + KeyWords[KeyWords.Length-1]+"\" ";

            //MySqlCmdFacility += " group by projectid ";

            if (OrderByList.Text == "Lab" || OrderByList.Text == "OrderBy")
                MySqlCmdFacility += " order by labname";
            else if (OrderByList.Text == "Title")
                MySqlCmdFacility += " order by ProjectTitle";
            else
                MySqlCmdFacility += " order by startdate";


            List<SearchItem> TempPrIds = new List<SearchItem>();
            List<SearchItem> SecondTempPrIds = new List<SearchItem>();

            MySqlCommand selectProject = new MySqlCommand(MySqlCmdFacility, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectProject);
            System.Data.DataSet PrIDsDataset = new System.Data.DataSet();
            adapter.Fill(PrIDsDataset, "PrResults");
            SearchItem TempItem;
            SearchItem ItemInthelist;
            foreach (DataRow row in PrIDsDataset.Tables["PrResults"].Rows)
            {
                TempItem = new SearchItem(row[1].ToString(), Int16.Parse(row[0].ToString()), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[8].ToString(), row[9].ToString());
                SecondTempPrIds.Add(TempItem);
                bool etst = TempPrIds.Contains(TempItem);
                if (TempPrIds.Contains(TempItem) && !PrIdsList.Contains(Initial))
                {
                    //TempPrIds.Remove(TempItem);
                    ItemInthelist = TempPrIds.Find((ee) => { return ee.ProjectID == TempItem.ProjectID; });
                    ItemInthelist.Count++;
                    ItemInthelist.Level.Add(TempItem.Level[0]); 
                    TempItem.Count++;
                   
                }
                else if (!TempPrIds.Contains(TempItem) && !PrIdsList.Contains(Initial))
                {
                    TempPrIds.Add(TempItem);
                }
                else if (PrIdsList.Contains(Initial))
                {
                    TempPrIds.Add(TempItem);
                    PrIdsList.Remove(Initial);
                }
            }
            PrIdsList = TempPrIds;
            FillSearchTree();
            SearchTree.Visible = true;
         

        
        }
        protected void BtnClick_Login(object sender, EventArgs e) 
        {
            conn.Open();
            MySqlCommand SelectCredential = new MySqlCommand("select Username,Password from Users where username=  @username And password = @Password", conn);
            SelectCredential.Parameters.AddWithValue("@username",TxtBxUsername.Text );
            SelectCredential.Parameters.AddWithValue("@password", TxtBxPassword.Text);
            MySqlDataReader CredentialsDataReader = SelectCredential.ExecuteReader();

            if (CredentialsDataReader.Read() == true)
            {
                //Credential do exist)
                lbl_WelcomeMsg.Text = "Partner: " + CredentialsDataReader[0].ToString()+"| ";
                LoggedDiv.Style.Add("display", "inline");
                LoginDiv.Style.Add("display", "none");
                AddPartnerProjects();
                div_wrongPass.Style.Add("display", "none");
               
               // Session["ActiveView"] = "Home";
                Session["LeftState"] = MoveToTheNextState("partner", Session["LeftState"].ToString());

            }
            else
            {

                LoggedDiv.Style.Add("display", "none");
                //LoginDiv.Style.Add("display", "inline");
               
                div_wrongPass.Style.Add("display", "inline");
                //Response.Write("<script type='text/javascript'>detailedresults=alert(\"Wrong Password!Please Try again.\");</script>");
                //Session["ActiveView"] = "Home";

            }

            conn.Close();
            MovetoTheNextPanelState(null, "Login");
        }

        protected void LnkBtnAdvSearch_Click(object sender, EventArgs e)
        {

           // SearchTree.FindNode("Search Results").ChildNodes.Clear();
           // GoogleDivs.InnerHtml = "";
            //PrIdsList.Add(Initial);
            Session["LeftState"] = MoveToTheNextState("Asearch", Session["LeftState"].ToString());
            MovetoTheNextPanelState(null, "Asearch");
        }
        protected void DropDownChgd(Object sender, EventArgs e)
        {

            //if it is visible it means that I already have some result PrIds
            //I have to re-order them
            if(SearchTree.Visible==true)
            {
                GetTreeNodesIds(OrderByList.Text);   
            }
           

            TreeNode TreeNodeToBeChanged ;
            if (OrderByList.Text.Equals("Lab") || OrderByList.Text.Equals("Order By"))
            {
                

                TreeNodeToBeChanged = TreeView2.FindNode("RootNode");
                TreeNodeToBeChanged.ChildNodes.Clear();
                FillLabs("public", TreeNodeToBeChanged);

                if (PartnerTree.Visible == true)
                {
                    PartnerTree.FindNode("Partner root").ChildNodes.Clear();
                    TreeNodeToBeChanged = PartnerTree.FindNode("Partner root");
                    TreeNodeToBeChanged.ChildNodes.Clear();
                    FillLabs("partner", TreeNodeToBeChanged);
                }
            }
            else
            {
                TreeNodeToBeChanged = TreeView2.FindNode("RootNode");
                TreeNodeToBeChanged.ChildNodes.Clear();
                FillTree("PUBLIC", OrderByList.Text, TreeNodeToBeChanged);

                if (PartnerTree.Visible == true)
                {
                    TreeNodeToBeChanged = PartnerTree.FindNode("Partner root");
                    TreeNodeToBeChanged.ChildNodes.Clear();
                    FillTree("partner", OrderByList.Text, TreeNodeToBeChanged);
                }
            }
        }

        protected void GetTreeNodesIds(string option) 
        {
            TreeNodeCollection PrNodes = new TreeNodeCollection();
            int Lengthtoken;
            TreeNodeCollection CurrentPrNodes = new TreeNodeCollection();
            string query = "select * from project where ";
               
               // SearchTree.FindNode("Search Results/" + "LabId:" + row[5].ToString());
                TreeNodeCollection LabNodes = SearchTree.FindNode("Search Results").ChildNodes;
                if (LabNodes[0].Value.Contains("Lab"))
                {
                   
                    for (int k = 0; k < LabNodes.Count; k++)
                    {
                        CurrentPrNodes = LabNodes[k].ChildNodes;


                        for (int i = 0; i < CurrentPrNodes.Count - 1; i++)
                        {
                            query += " (idProject = " + CurrentPrNodes[i].Value.Substring(10) + " or";
                        }

                    }
                    query += " idProject = " + CurrentPrNodes[CurrentPrNodes.Count - 1].Value.Substring(10);
                    query += " )";
                }
                else
                {
                    

                    for (int i = 0; i < LabNodes.Count - 1; i++)
                    {
                        query += " (idProject = " + LabNodes[i].Value.Substring(10) + " or";
                    }
                    query += " idProject = " + LabNodes[LabNodes.Count - 1].Value.Substring(10);
                    query += " )";
                }
           


            SearchTree.FindNode("Search Results").ChildNodes.Clear();

            string query2 = "select * from laboratory";
            MySqlConnection conn = new MySqlConnection("server=localhost;User Id=root;database=newseriesserver;password=root;");
            MySqlConnection conn2 = new MySqlConnection("server=localhost;User Id=root;database=newseriesserver;password=root;");
            if (option == "Lab")
            {
                MySqlCommand selectLabs = new MySqlCommand(query2, conn);
                MySqlDataAdapter Labadapter = new MySqlDataAdapter(selectLabs);
                System.Data.DataSet LabDataset = new System.Data.DataSet();
                Labadapter.Fill(LabDataset);

                if (LabDataset.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow row in LabDataset.Tables[0].Rows)
                    {
                        conn2.Open();
                        

                        MySqlCommand selectProjects = new MySqlCommand(query + " and laboratory_idlaboratory = " + row[5].ToString(), conn2);

                        MySqlDataAdapter adapter = new MySqlDataAdapter(selectProjects);
                        System.Data.DataSet projects = new System.Data.DataSet();
                        adapter.Fill(projects);

                        if ((projects.Tables.Count > 0) && (projects.Tables[0].Rows.Count>0))
                        {
                            TreeNode LabNode = new TreeNode(row[0].ToString(), "LabId:" + row[5].ToString());
                            SearchTree.FindNode("Search Results").ChildNodes.Add(LabNode);
                        
                            foreach (System.Data.DataRow PrRow in projects.Tables[0].Rows)
                            {
                                
                                AddProjectToTree(PrRow[0].ToString(), PrRow[2].ToString(), SearchTree.FindNode("Search Results/" +"LabId:"+ row[5].ToString()));

                            }
                        }
                        conn2.Close();

                    }
                }
            }
            else
            {

                if (option == "StartDate")
                    query += " order by StartDate";
                else if (option=="Title")
                    query += " order by Title";

                MySqlCommand selectProjects = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(selectProjects);
                System.Data.DataSet projects = new System.Data.DataSet();
                adapter.Fill(projects);

                if (projects.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow row in projects.Tables[0].Rows)
                    {
                        AddProjectToTree(row[0].ToString(), row[2].ToString(), SearchTree.FindNode("Search Results"));

                    }
                }
 
            }


            
   
        }

        protected void ImgLogout_Click(object sender, EventArgs e) 
        {
            LoggedDiv.Style.Add("display", "none");
            LoginDiv.Style.Add("display", "inline");
            div_wrongPass.Style.Add("display", "none");
            PartnerTree.Nodes[0].ChildNodes.Clear();
            //string test = Session["ActiveView"].ToString();
            Session["LeftState"] = MoveToTheNextState("logout", Session["LeftState"].ToString());

            MovetoTheNextPanelState(null, "Logout");
 
        }
        protected void AddPartnerProjects()
        {
          //  TreeNode PartnerProjects = new TreeNode();
           // PartnerProjects.Text = "Partner root";
           // PartnerProjects.Value = "Partner root";
            //FillProject("partner");
            TreeNode TreeNodeToBeChanged  = PartnerTree.FindNode("Partner root");
            TreeNodeToBeChanged.ChildNodes.Clear();
            PartnerTree.Visible = true;

            string orderbyvalue = GetDropDownValue();
            if (orderbyvalue.Equals("Lab"))
                FillLabs("Partner", TreeNodeToBeChanged);
            else
                FillTree("Partner", orderbyvalue, TreeNodeToBeChanged);
           
          
        }

        protected string FindSearchProjectIDs(ListBox ListBox_Search, string category)
        {
            string Level = null;
            string ValuePathSeparator = null;
            string DownloadLink;
            string GeneralInfoLink;

          
            string MySqlCmdFacility = "select DISTINCT search.projectid, Search.projectTitle, Search.ParentID,Search.Category, search.abstract,search.startdate, search.enddate,search.specimenid,search.compexpid,search.signalid from search where ";
            //SelectedCategories += category + ", ";
            MySqlCmdFacility += "( search.category = \"" + category + "\" ";
            for (int i = 0; i < ListBox_Search.Items.Count; i++)
            {

                //gets the lowest selected   index--the first one has to be connecteed with AND
                if (ListBox_Search.SelectedIndex == i)
                {
                    MySqlCmdFacility += " AND ( search.keyword = \"" + ListBox_Search.Items[i].Value + "\" ";
                    ValuePathSeparator += ListBox_Search.Items[i].Value;
                }//all the other selected items has to be selected with OR
                else if (ListBox_Search.Items[i].Selected)
                {
                    MySqlCmdFacility += " OR   search.keyword = \"" + ListBox_Search.Items[i].Value + "\" ";
                    ValuePathSeparator += ", " + ListBox_Search.Items[i].Value;

                }
            }
            MySqlCmdFacility += " ) )  ";

          
            return ValuePathSeparator;
        }
        public string BuildSearchQuery(ListBox CurrentListBox, string Category)
        {
            string MySqlCmdFacility = null;
            if (CurrentListBox.SelectedIndex != -1)
            {
          
                MySqlCmdFacility += " search.category = \"" + Category + "\" ";

                for (int i = 0; i < CurrentListBox.Items.Count; i++)
                {
                    if (CurrentListBox.SelectedIndex == i)
                    {
                        MySqlCmdFacility += " AND ( search.keyword = \"" + CurrentListBox.Items[i].Value + "\" ";
                    }
                    else if (CurrentListBox.Items[i].Selected)
                        MySqlCmdFacility += " OR   search.keyword = \"" + CurrentListBox.Items[i].Value + "\" ";
                }
                MySqlCmdFacility += " ) )  ";
            }
            return MySqlCmdFacility;

        }
        public void NewBuildSearchQuery(ListBox CurrentListBox, string Category)
        {
            string MySqlCmdFacility = null;
            
            if (CurrentListBox.SelectedIndex != -1)
            {
                MySqlCmdFacility = "select DISTINCT projectid, projectTitle, abstract,startdate,enddate, labname,labid,specimenid,compexpid,signalid from search where category = \"" + Category + "\" AND ( ";

                ValuePathSeparator += "<b>"+Category+":</b>";
                for (int i = 0; i < CurrentListBox.Items.Count ; i++)
                {
                    if (CurrentListBox.Items[i].Selected && CurrentListBox.SelectedIndex != i)
                    {
                        MySqlCmdFacility += " search.keyword = \"" + CurrentListBox.Items[i].Value + "\" OR";
                        ValuePathSeparator +=  CurrentListBox.Items[i].Value + ", ";
                    }
                }
                MySqlCmdFacility += " search.keyword = \"" + CurrentListBox.SelectedValue + " \" )";
                ValuePathSeparator += CurrentListBox.SelectedValue + "<br/>";

             
            }
           


            List<SearchItem> TempPrIds = new List<SearchItem>();
            List<SearchItem> SecondTempPrIds = new List<SearchItem>();

            MySqlCommand selectProject = new MySqlCommand(MySqlCmdFacility, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectProject);
            System.Data.DataSet PrIDsDataset = new System.Data.DataSet();
            adapter.Fill(PrIDsDataset, "PrResults");
            SearchItem TempItem;
            SearchItem ItemInthelist; 
            foreach (DataRow row in PrIDsDataset.Tables["PrResults"].Rows)
            {
                TempItem = new SearchItem(row[1].ToString(), Int16.Parse(row[0].ToString()), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[8].ToString(), row[9].ToString());
                //SecondTempPrIds.Add(TempItem);
                if (TempPrIds.Contains(TempItem) && !PrIdsList.Contains(Initial))
                {
                    //TempPrIds.Remove(TempItem);
                    ItemInthelist =  TempPrIds.Find((e) => { return e.ProjectID == TempItem.ProjectID; });
                    ItemInthelist.Count++;
                    ItemInthelist.Level.Add(TempItem.Level[0]); 
                    TempItem.Count++;
                    //TempPrIds.Add(TempItem);

                }
                else if (!TempPrIds.Contains(TempItem) && !PrIdsList.Contains(Initial))
                {
                    TempPrIds.Add(TempItem);
                }
                else if (PrIdsList.Contains(Initial))
                {
                    TempPrIds.Add(TempItem);
                    PrIdsList.Remove(Initial);
                }

            }



            if(PrIdsList.Count>0)
            {
           
                foreach (SearchItem newItem in TempPrIds)
                {
                    if(PrIdsList.Contains(newItem))
                    {
                        ItemInthelist = PrIdsList.Find((e) => { return e.ProjectID == newItem.ProjectID; });
                        ItemInthelist.Count += newItem.Count;
                        ItemInthelist.Level.AddRange(newItem.Level);
                       //ItemInthelist.Level = ItemInthelist.Level.Distinct().ToList();
                        SecondTempPrIds.Add(ItemInthelist);
                    }
                }
                PrIdsList = SecondTempPrIds;

            }else
                PrIdsList = TempPrIds;

           // TempPrIds.Clear();
            //SecondTempPrIds.Clear();





        }
        protected void NewSearchFunction(object sender, EventArgs e)
        {
            NumPrRes = 0;
            NumSpecRes = 0;
            NumCompExpRes = 0;
            NumSigRes = 0;
            MovetoTheNextPanelState(null, "Asearch");
//            Session["ClickedTree"] = "SearchTree";
            Session["LeftState"] = MoveToTheNextState("search", Session["LeftState"].ToString());




            GoogleDivs.InnerHtml = "";
            PrIdsList.Add(Initial);


            ValuePathSeparator = "<b>Search Criteria:</b><br/>";
            //make it visible
            SearchTree.Visible = true;
            //if this is not first time search tree has results so it has to be cleared
            SearchTree.FindNode("Search Results").ChildNodes.Clear();

           string RunningQuery = null ;
            
            if (ListBox_Location.SelectedIndex != -1)
            {
              
                NewBuildSearchQuery(ListBox_Location, "Location");
              
        
            }
            if (ListBox_Infrastructure.SelectedIndex != -1)
            {
         
                NewBuildSearchQuery(ListBox_Infrastructure, "Infrastructure") ;
                
                //ValuePathSeparator += "<b>Infrastructure:</b>" + RunningQuery + "<br/>";
            }

            if (ListBox__ResearchArea.SelectedIndex != -1)
            {
                NewBuildSearchQuery(ListBox__ResearchArea, "ResearchArea");
                
                //ValuePathSeparator += "<b>ResearchArea:</b>" + RunningQuery + "<br/>";

            }


            if (ListBox_PrincipleExpType.SelectedIndex != -1)
            {
                NewBuildSearchQuery(ListBox_PrincipleExpType, "PrincipleExpType");
                
                //ValuePathSeparator += "<b>PrincipleExperimentType:</b>" + RunningQuery + "<br/>";
            }

            if (ListBox_StructuralElement.SelectedIndex != -1)
            {
                NewBuildSearchQuery(ListBox_StructuralElement, "Structural Element") ;
                
                //ValuePathSeparator += "<b>StructuralElement:</b>" + RunningQuery + "<br/>";
            }
            // if (ListBox_Spec_Picture.SelectedIndex != -1)
            // {
            //    ValuePathSeparator += "<b>specimen Pictures:</b>" + FindSearchProjectIDs(ListBox_Spec_Picture, "specimen pictures") + "<br/>";
            // }

            if (ListBox_ExpType.SelectedIndex != -1)
            {
                NewBuildSearchQuery(ListBox_ExpType, "exptype");
                
                //ValuePathSeparator += "<b>expType:</b>" + RunningQuery + "<br/>";
            }

            if (ListBox_ExpOLS_Nature.SelectedIndex != -1)
            {
                NewBuildSearchQuery(ListBox_ExpOLS_Nature, "OLS_NATURE") ;
                
                //ValuePathSeparator += "<b>OLS Nature:</b>" + RunningQuery + "<br/>";
            }

            if (ListBox_ExpOLS_Source.SelectedIndex != -1)
            {  
                NewBuildSearchQuery(ListBox_ExpOLS_Source, "OLS_Source");
                
                //ValuePathSeparator += "<b>OLS Source:</b>" + RunningQuery + "<br/>";
            }

            if (ListBox_CompType.SelectedIndex != -1)
            {
                NewBuildSearchQuery(ListBox_CompType, "CompType") ;
                
                //ValuePathSeparator += "<b>Computation Type:</b>" + RunningQuery  + "<br/>";
            }


            if (ListBox_Signal_PhysicalQ.SelectedIndex != -1)
            {
                NewBuildSearchQuery(ListBox_Signal_PhysicalQ, "PhysicalQ") ;
                
                //ValuePathSeparator += "<b>Physical Q:</b>" + RunningQuery + "<br/>";
            }


            

            if (ListBox_Acronym.SelectedIndex != -1)
            {
                NewBuildSearchQuery(ListBox_Infrastructure, "Infrastructure") ;
                
                //ValuePathSeparator += "<b>Acronym:</b>" + RunningQuery + "<br/>";
            }

            if (ListBox_Investigator.SelectedIndex != -1)
            {
                NewBuildSearchQuery(ListBox_Investigator, "investigator") ;
                
                //ValuePathSeparator += "<b>Investigator:</b>" + RunningQuery + "<br/>";
            }

            if (ListBox_CompSoft.SelectedIndex != -1)
            {
               NewBuildSearchQuery(ListBox_CompSoft, "CompSoft");
               
               //ValuePathSeparator += "<b>Computation Software:</b>" + RunningQuery + "<br/>";
            }

            if (ListBox_Material.SelectedIndex != -1)
            {
                NewBuildSearchQuery(ListBox_Material, "resource");

                //ValuePathSeparator += "<b>Computation Software:</b>" + RunningQuery + "<br/>";
            }



            FillSearchTree();
         
            //Search_Criteria.InnerHtml = "<br/><br/><br/><br/><br/><br/><b>Results Found: </b>" + NumPrRes + " Results at the project level, " + NumSpecRes + " at the specimen level, " + NumCompExpRes + " at the Computation Experiment level, " + NumSigRes + "at the signal Level.</br>Navigate through the results via the left panel</br></br>";
            
            //Search_Criteria.InnerHtml += ValuePathSeparator;
            h4SearchPr.InnerHtml = "<a class=\"collapsed\">&nbsp; &nbsp; &nbsp;  Project</a>";
            DivSearchPr.Style.Add("display", "none");
            h4SearchSpec.InnerHtml = "<a class=\"collapsed\">&nbsp; &nbsp; &nbsp;  specimen</a>";
            DivSearchSpec.Style.Add("display", "none");
            h4SearchCompExp.InnerHtml = "<a class=\"collapsed\">&nbsp; &nbsp; &nbsp;  Comp/Exp</a>";
            DivSearchCompExp.Style.Add("display", "none");
            h4SearchSignal.InnerHtml = "<a class=\"collapsed\">&nbsp; &nbsp; &nbsp;  Signal</a>";
            DivSearchSignal.Style.Add("display", "none");




        }
        
        public void FillSearchTree() 
        {


            string Level = null;
            string DownloadLink;
            string GeneralInfoLink;

            Search_Criteria.InnerHtml = ValuePathSeparator;
            Search_Criteria.InnerHtml += "<br/><b>Results Found: </b>";

            if (PrIdsList.Count == 0)
                Search_Criteria.InnerHtml = "No results found";
            
            foreach (SearchItem item in PrIdsList)
            {


                item.Level = item.Level.Distinct().ToList();
                //start creating the divs for each results
                //check if node exists
                //insted of this call the color function and return the parent-parent-parent-parent....
                Search_Criteria.InnerHtml += "<br/>" + item.Count + " results at " + item.ProjectTitle + "( level: " ;
                foreach(string currentlevel in item.Level)
                {
                    Search_Criteria.InnerHtml += currentlevel + ", " ;
                }
           
                Search_Criteria.InnerHtml += " )";
                
                TreeNode ProjectNode = SearchTree.FindNode("Search Results/ProjectID:" + item.ProjectID);
              
                 ProjectNode = new TreeNode();
                 ProjectNode.Text = item.ProjectTitle;
                 ProjectNode.Value = "ProjectID:" + item.ProjectID;
                 ProjectNode.ImageUrl = "newcss/Folder.gif";

                 TreeNode SpecimenLevelNode = new TreeNode();
                 TreeNode ExpLevelNode = new TreeNode();
                 TreeNode CompLevelNode = new TreeNode();
                 TreeNode SignalLevelNode = new TreeNode();

                 SpecimenLevelNode.Text = "Specimen Level";
                 SpecimenLevelNode.Value = "SpecimenLevelID:" + item.ProjectID;
                 SpecimenLevelNode.ImageUrl = "newcss/Folder.gif";

                 ExpLevelNode.Text = "Experiment Level";
                 ExpLevelNode.Value = "ExpLevelID:" + item.ProjectID;
                 ExpLevelNode.ImageUrl = "newcss/Folder.gif";

                 CompLevelNode.Text = "Computation Level";
                 CompLevelNode.Value = "CompLevelID:" + item.ProjectID;
                 CompLevelNode.ImageUrl = "newcss/Folder.gif";

                 SignalLevelNode.Text = "Signal Level";
                 SignalLevelNode.Value = "SignalLevelID:" + item.ProjectID;
                 SignalLevelNode.ImageUrl = "newcss/Folder.gif";


                 ProjectNode.ChildNodes.Add(SpecimenLevelNode);
                 ProjectNode.ChildNodes.Add(ExpLevelNode);
                 ProjectNode.ChildNodes.Add(CompLevelNode);
                 ProjectNode.ChildNodes.Add(SignalLevelNode);

                 if (item.Level.Contains("specimen"))
                    FillSpecimens(SpecimenLevelNode, "Specimen Level",item.ParentId);
                else
                    FillSpecimens(SpecimenLevelNode, "Specimen Level");

                if (item.Level.Contains("experiment"))
                    FillSpecimens(ExpLevelNode, "Experiment Level", item.ParentId);

                else
                    FillSpecimens(ExpLevelNode, "Experiment Level");


                if (item.Level.Contains("computation"))
                    FillSpecimens(CompLevelNode, "Computation Level", item.ParentId);
                else
                    FillSpecimens(CompLevelNode, "Computation Level");

                if (item.Level.Contains("signal"))
                    FillSpecimens(SignalLevelNode, "Signal Level", item.ParentId);
                else
                    FillSpecimens(SignalLevelNode, "Signal Level");



                if (item.Level.Contains("project"))
                    ProjectNode.Text = "<font style='color:#A52A2A;'>" + ProjectNode.Text + "</font>";

                 ProjectNode.Collapse();
                 TreeNode LabNode;
                 if (OrderByList.Text == "Lab" || OrderByList.Text == "OrderBy")
                 {
                     LabNode = SearchTree.FindNode("Search Results/LabID:" + item.LabId);
                     if (LabNode == null)
                     {
                         LabNode = new TreeNode(item.LabName, "LabID:" + item.LabId);
                         SearchTree.FindNode("Search Results").ChildNodes.Add(LabNode);
                     
                     }
                    LabNode.ChildNodes.Add(ProjectNode);

                 }
                 else 
                 {
                     SearchTree.FindNode("Search Results").ChildNodes.Add(ProjectNode);
 
                 }
                    
                //   SearchTree.FindNode("Search Results").ChildNodes.Add(ProjectNode);

                
                
                
       
               // ColorNode(row[0].ToString(), row[7].ToString(), row[8].ToString(), row[9].ToString(), Level);



                //TempNode.Text = "<font style='color:#A52A2A;'>" + TempNode.Text + "</font>";
                //TreeNode SpecimenLevelNode = new TreeNode();
                //TreeNode CompExpLevelNode = new TreeNode();
                //TreeNode LevelNode = new TreeNode();

                //if node does not exist add it to the SearchTree
                //call the overloaded fillspecimen with the level as parameter 



                TreeNodesCount++;
           
                //CHECK IF EXISTS-IN THE GLOBAL ARRAY
               // if (PrIds.Find(item => item == Convert.ToInt16(row[0].ToString())) == 0)
               // {

                DownloadLink = "<a href=\"javascript:document.getElementById('x').value='#Download';__doPostBack('SearchTree','sSearch Results\\\\ProjectID:" + item.ProjectID.ToString() + "')\" onclick=\"TreeView_SelectNode(SearchTree_Data, this,'SearchTreet" + TreeNodesCount + "');\" id=\"SearchTreet" + TreeNodesCount + "\">";
                    GeneralInfoLink = "<a href=\"javascript:__doPostBack('SearchTree','sSearch Results\\\\ProjectID:" + item.ProjectID.ToString() + "')\" onclick=\"TreeView_SelectNode(SearchTree_Data, this,'SearchTreet" + TreeNodesCount + "');\" id=\"SearchTreet" + TreeNodesCount + "\">";
                    PrIds.Add(Convert.ToInt16(item.ProjectID.ToString()));
                    GoogleDivs.InnerHtml += "<div style=\"margin-left:50px; border: 1px solid #999;background-color: #F5F5F5;width:90%\" >" + CountPrResults + ".Project title: ";
                    GoogleDivs.InnerHtml += GeneralInfoLink;
                    GoogleDivs.InnerHtml += "<label >" + item.ProjectTitle + "</label></a><label style=\"float:right;\">" + DownloadLink + "Go to download </a></label><br/>" + "Start Date: <label>" + item.StartDate + "</label>" + "  End Date: <label>" + item.EndDate + "</label></a>" + "<br/><br/><u>Description</u>: " + item.Description + "</div></br>";
                    GoogleDivs.InnerHtml += "";
                    CountPrResults++;

                //}
            }
            Search_Criteria.InnerHtml += "<br/>";
                
            Search_Criteria.Visible = true;

            /*LEITOURGEI TO SVINW PROSORINA
               TreeNodesCount += ProjectNode.ChildNodes.Count;

               foreach (TreeNode SpecNode in ProjectNode.ChildNodes)
               {
                   //cound the Comp Exp
                   TreeNodesCount += SpecNode.ChildNodes.Count;

                   foreach (TreeNode CompExp in SpecNode.ChildNodes)
                   {
                       //count the signals
                       TreeNodesCount += CompExp.ChildNodes.Count;
                   }
               }


           */


            
 
        }
      

        protected void BtnDownload_Click(object sender, EventArgs e)
        {
            ControlCollection CheckedGridviews = DownloadPr_View.Controls;
            StringBuilder str = new StringBuilder();
            string category, localID;

            foreach (Control CheckedGridView in CheckedGridviews)
            {

                if (CheckedGridView.GetType().Equals(typeof(GridView)))
                {
                    GridView RunningGrid = (GridView)CheckedGridView;
                    // Select the checkboxes from the GridView control

                    for (int i = 0; i < RunningGrid.Rows.Count; i++)
                    {
                        GridViewRow row = RunningGrid.Rows[i];

                        string temp = RunningGrid.ID;
                        string CheckName = "chkSelect" + temp.Substring(8, temp.Length - 13); ;
                        bool isChecked = ((CheckBox)row.FindControl(CheckName)).Checked;

                        if (isChecked)
                        {
                            // Column 2 is the name column
                            category = GetDownloadCategory(RunningGrid.ID);
                            localID = RunningGrid.DataKeys[i].Value.ToString();
                            // localID = DownloadPrDoc_GrdV.Rows[i].Cells[DownloadPrDoc_GrdV.Columns.Count - 2].Text;
                            //DownloadFile(localID, category, "UOXF", "OK");

                        }
                    }
                }
            }


            // prints out the result

            //Session["ActiveView"] = "Download";
            MovetoTheNextPanelState(null, "Download");


        }
        protected  bool ColorNode(string ProjectID, string SpecimenID,string CompExpID,string SignalID, string Level) 
        {
            TreeNode TempNode = new TreeNode();
            if (Level.Equals("Project Level"))
            {
                TempNode = SearchTree.FindNode("Search Results/ProjectID:" + ProjectID);
                
            }
            else if (Level.Equals("Specimen Level"))
            {
                TempNode = SearchTree.FindNode("Search Results/ProjectID:" + ProjectID + "/SpecimenLevelID:" + ProjectID + "/SpecimenID:" + SpecimenID);
            }
            else if (Level.Equals("CompExp Level"))
            {
                TempNode = SearchTree.FindNode("Search Results/ProjectID:" + ProjectID + "/ExpLevelID:" + ProjectID + "/SpecimenID:" + SpecimenID + "/CompExpID:" + CompExpID);
                if(TempNode == null)
                    TempNode = SearchTree.FindNode("Search Results/ProjectID:" + ProjectID + "/CompLevelID:" + ProjectID + "/SpecimenID:" + SpecimenID + "/CompExpID:" + CompExpID);
            }
            else if (Level.Equals("Signal Level"))
            {
                TempNode = SearchTree.FindNode("Search Results/ProjectID:" + ProjectID + "/SignalLevelID:" + ProjectID + "/SpecimenID:" + SpecimenID + "/CompExpID:" + CompExpID + "/SignalID:" + SignalID);
            }

            if (TempNode != null)
            {
                TempNode.Text = "<font style='color:#A52A2A;'>" + TempNode.Text + "</font>";
                return true;
            }
            else
                return false;
            
        }

        //BtnDownAll_Click
        protected void BtnDownAll_Click(object sender, EventArgs e)
        {
            ControlCollection Gridviews = DownloadPr_View.Controls;
            StringBuilder str = new StringBuilder();
            string category, localID;

            foreach (Control DownGridView in Gridviews)
            {

                if (DownGridView.GetType().Equals(typeof(GridView)))
                {
                    GridView RunningGrid = (GridView)DownGridView;
                    // Select the checkboxes from the GridView control

                    for (int i = 0; i < RunningGrid.Rows.Count; i++)
                    {
                        GridViewRow row = RunningGrid.Rows[i];

                        // Column 2 is the name column
                        category = GetDownloadCategory(RunningGrid.ID);
                        localID = RunningGrid.DataKeys[i].Value.ToString();
                        // localID = DownloadPrDoc_GrdV.Rows[i].Cells[DownloadPrDoc_GrdV.Columns.Count - 2].Text;
                      //  DownloadFile(localID, category, "UOXF", "OK");
                    }
                }
            }


            // prints out the result

            Session["ActiveView"] = "Download";



        }
               
        protected void DownloadFile(string id, String cat, string dom, string par,string ip)
        {
            string rqd = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss");
            UTF8Encoding ByteConverter = new UTF8Encoding();
            // Create the data to be signed
            string DataToBeSigned = rqd + id + cat + dom + par;
            byte[] dataBytes = ByteConverter.GetBytes(DataToBeSigned);
            //data to be signed
            byte[] signedData = null;


            string DigitalCertificateName = "CN=150.140.188.205, OU=HCILab, O=University, L=patra, S=Akhaia, C=GR";
            //load the certificate
            X509Certificate2 CurentCert = new X509Certificate2(@"C:\code\SignData.pfx");//X509Store(StoreName.My, StoreLocation.LocalMachine);

            if (CurentCert.Subject == DigitalCertificateName)
            {
                //load the private key
                RSACryptoServiceProvider RSA = (RSACryptoServiceProvider)CurentCert.PrivateKey;
                //sign the data with private key
                signedData = RSA.SignData(dataBytes, new SHA1CryptoServiceProvider());

            }
            //convert to base64
            string signature = System.Convert.ToBase64String(signedData);
            //fix it so as to be ok for web transimition
            signature = System.Web.HttpUtility.UrlEncode(signature);

            string urlPart = @"http://" + ip + ":443/DatOX/Download?";
            string FinalUrl = urlPart + "rqd=" + rqd + "&id=" + id + "&cat=" + cat + "&dom=" + dom + "&par=" + par + "&signature=" + signature;

            //Response.Redirect(FinalUrl);
            Response.Write("<script type='text/javascript'>detailedresults=window.open('" + FinalUrl + "');</script>");

        }
        protected string GetDownloadCategory(string GridViewName)
        {
            string cat = null;

            switch (GridViewName)
            {
                case "DownloadPrDoc_GrdV":
                    cat = "DOC";
                    break;
                case "DownloadSpecDoc_GrdV":
                    cat = "DOC";
                    break;
                case "DownloadSpecImg_GrdV":
                    cat = "IMG";
                    break;
                case "DownloadMeshModelDoc_GrdV":
                    cat = "DOC";
                    break;
                case "DownloadMeshModelImg_GrdV":
                    cat = "IMG";
                    break;
                case "DownloadExpImg_GrdV":
                    cat = "IMG";
                    break;
                case "DownloadExpDoc_GrdV":
                    cat = "DOC";
                    break;
                case "DownloadExpVid_GrdV":
                    cat = "VID";
                    break;
                case "DownloadSignalResult_GrdV":
                    cat = "SIG";
                    break;
                case "DownloadOLS_Signal_GrdV":
                    cat = "SIG";
                    break;
            }
            return cat;
        }
        protected void OnRowCommand(object sender, GridViewCommandEventArgs e)
        {

            GridView ClickedGridView = (GridView)sender;
            //get the clicked row

            GridViewRow ClickedRow = ClickedGridView.Rows[Convert.ToInt32(e.CommandArgument)];
            // localID = DownloadPrDoc_GrdV.Rows[i].Cells[DownloadPrDoc_GrdV.Columns.Count - 2].Text;
            string category = GetDownloadCategory(ClickedGridView.ID);
            string localID = ClickedGridView.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            //string[] test = ClickedGridView.DataKeyNames;
            string[] labdata = GetLabName();
            //check this with the name
            Terms_Of_Use(localID, category, labdata[1], "OK", labdata[0]);
           // DownloadFile(localID, category, "UOXF", "OK", "163.1.8.139");
        }

        protected void Terms_Of_Use(string localID,string category, string lab,string status,string ip) 
        {
            //Response.Redirect(FinalUrl);
            string param = "localid=" + localID + "&category=" + category + "&lab=" + lab + "&status=" + status+ "&ip=" +  ip ;
            Response.Write("<script type='text/javascript'>detailedresults=window.open('Terms_Of_Use.aspx?"+ param+"');</script>");
        }
        protected void ProjectMenu_MenuItemClick(object sender ,MenuEventArgs e) 
        {

            MovetoTheNextPanelState(null, "LoadData_General");
            if (ProjectMenu.SelectedItem.Text.Contains("Specimen"))
            {
                //PrTitleLabel.Text = test.SelectedNode.Parent.Text;

                MultiView1.ActiveViewIndex = 6;
               
                DataBindGridView(GridSpecLevelSpecData, H4SpecData, "Specimen Data");
                DataBindGridView(GridSpecLevelSpecDocs, H4SpecDoc, "Specimen Document");
                DataBindGridView(GridSpecLevelSpecIMGs, H4SpecImg, "Specimen Images");
                DataBindGridView(GridSpecLevelSpecScaling, H4SpecScaling, "Specimen Scaling");
                DataBindGridView(GridSpecLevelStrElemData, H4SpecStrElem, "Structural Element Data");
                DataBindGridView(GridSpecLevelStrElemMatActProp, H4StrlElem_Mat_ActualProp, "Structual Element and Material Actual Properties");
                DataBindGridView(GridSpecLevelStrElemMaterial, H4SpecStrElemMat, "Structural Element and it's Material");
                DataBindGridView(GridSpecLevelStrElemMatNomProp, H4StrlElem_Mat_Nom_Prop, "Structural Elements and material Nominal Properties");

                FindSpecimenLevelView();


            }
            else if (ProjectMenu.SelectedItem.Text.Contains("Experiment"))
            {
                //PrTitleLabel.Text = test.SelectedNode.Parent.Text;

                MultiView1.ActiveViewIndex = 7;
                DataBindGridView(GridCompExpLevelPerson, H4ExpCompPerson, "Experiment Personnel ");
                DataBindGridView(GridCompExpLevelData, H4ExpCompData, "Experiment data");
                DataBindGridView(GridCompExpLevelDLC, H4ExpCompDLC, "Experiment Detailed Loading Characteristics");
                DataBindGridView(GridCompExpLevelDOC, H4ExpCompDoc, "Experiment Documents");
                DataBindGridView(GridCompExpLevelIMG, H4ExpCompIMGS, "Experimen Imaegs");
                DataBindGridView(GridCompExpLevelMeshModel, H4ExpCompMeshModel, "Mesh model");
                DataBindGridView(GridCompExpLevelMeshModelDoc, H4ExpCompMeshModelDoc, "Mesh Model Documents");
                DataBindGridView(GridCompExpLevelMeshModelIMG, H4ExpCompMeshModelIMG, "Mesh Model Images");
                DataBindGridView(GridCompExpLevelOLS, H4ExpCompOLS, "Original Loading Signals");
                DataBindGridView(GridCompExpLevelVideo, H4ExpCompVideo, "Experiment Video");
                DataBindGridView(GridCompExpLevelPC, H4ExpCompPC, "Computer System");
                DataBindGridView(GridCompExpLevelInputFile, H4ExpCompInputFile, "Effective Input Files");
                DataBindGridView(GridCompExpLevelOLS_Signals, H4ExpCompOLS_Signal, "OLS signal Attributes");
                

                FindCompExpLevelView();


            }
            else if (ProjectMenu.SelectedItem.Text.Contains("Computation"))
            {

                //PrTitleLabel.Text = test.SelectedNode.Parent.Text;

                MultiView1.ActiveViewIndex = 7;
                DataBindGridView(GridCompExpLevelPerson, H4ExpCompPerson, "Computation Personnel");
                DataBindGridView(GridCompExpLevelData, H4ExpCompData, "Computation data");
                DataBindGridView(GridCompExpLevelDLC, H4ExpCompDLC, "Computation Detailed Loading Characteristics");
                DataBindGridView(GridCompExpLevelDOC, H4ExpCompDoc, "Computation Documents");
                DataBindGridView(GridCompExpLevelIMG, H4ExpCompIMGS, "Computation Imaegs");
                DataBindGridView(GridCompExpLevelMeshModel, H4ExpCompMeshModel, "Mesh model");
                DataBindGridView(GridCompExpLevelMeshModelDoc, H4ExpCompMeshModelDoc, "Mesh Model Documents");
                DataBindGridView(GridCompExpLevelMeshModelIMG, H4ExpCompMeshModelIMG, "Mesh Model Images");
                DataBindGridView(GridCompExpLevelOLS, H4ExpCompOLS, "Original Loading Signals");
                DataBindGridView(GridCompExpLevelPC, H4ExpCompPC, "Computer System");
                DataBindGridView(GridCompExpLevelVideo, H4ExpCompVideo, "Computation Video");
                DataBindGridView(GridCompExpLevelInputFile, H4ExpCompInputFile, "Effective Input Files");
                DataBindGridView(GridCompExpLevelOLS_Signals, H4ExpCompOLS_Signal, "OLS signal Attributes");
                

                FindCompExpLevelView();



            }
            else if (ProjectMenu.SelectedItem.Text.Contains("Signal"))
            {

                //PrTitleLabel.Text = test.SelectedNode.Parent.Text;
                MultiView1.ActiveViewIndex = 8;
               
                DataBindGridView(GridSignalLevel, H4SignalData, "Signal Data");

                FindSignalLevelView();
            }
            else if (ProjectMenu.SelectedItem.Text.Contains("Project"))
            {

                //PrTitleLabel.Text = test.SelectedNode.Parent.Text;
                MultiView1.ActiveViewIndex = 2;

                DataBindGridView(GrdViewDetPrData, H4PrData, "Project Data");
                DataBindGridView(GrdViewDetPrInvest, H4PrInvestigator, "Project Investigators");
                DataBindGridView(GrdViewDetPrInfrastr, H4PrInfrstr, "Project Infrastructure");
                DataBindGridView(GrdViewDetPrDoc, H4PrDoc, "Project Documents");

                FindProjectLevelView();
                //DataBindGridView(GridView19, H4PrMainFocus, "Project Main Focus");
               // DataBindGridView(GridView20, H4PrExpType, "Project Experiment Type");
            }
            
            
        }
        protected void BindSpecimenGridView(int ViewID) 
        {

            MultiView1.ActiveViewIndex = 6;
            GridSpecLevelSpecData.DataBind();
            GridSpecLevelSpecDocs.DataBind();
            GridSpecLevelSpecIMGs.DataBind();
            GridSpecLevelSpecScaling.DataBind();
            GridSpecLevelStrElemData.DataBind();
            GridSpecLevelStrElemMatActProp.DataBind();
            GridSpecLevelStrElemMaterial.DataBind();
            GridSpecLevelStrElemMatNomProp.DataBind();
                
        }

        protected void SpecLevel_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            string ctrlname = Page.Request.Params.Get("__EVENTTARGET");


            if (ctrlname.Contains("Tree")&&((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Text.Equals(ClickedNode.Text))))
            {
                // Display the company name in italics.
                e.Row.CssClass = "HighlightedRow";
                //e.Row.Cells[1].Text = "<b>" + e.Row.Cells[1].Text + "</b>";

            }

        }
        protected void ExpLevel_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            string ctrlname = Page.Request.Params.Get("__EVENTTARGET");


            if (ctrlname.Contains("Tree") && (e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[2].Text.Equals(ClickedNode.Text)))
            {
                // Display the company name in italics.
                e.Row.CssClass = "HighlightedRow";
                //e.Row.Cells[1].Text = "<b>" + e.Row.Cells[1].Text + "</b>";

            }
            else if (ctrlname.Contains("Tree") && (e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Text.Equals(ClickedNode.Text)))
            {
                e.Row.CssClass = "HighlightedRow";
            }
            else if (ctrlname.Contains("Tree") && (e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[3].Text.Equals(ClickedNode.Text)))
            {
                e.Row.CssClass = "HighlightedRow";
            }
         

        }

        protected void DataBindGridView(GridView GrdView,HtmlGenericControl h4, string Title) 
        {
            GrdView.DataBind();
            h4.InnerHtml= "<a class=\"expanded\">&nbsp &nbsp &nbsp &nbsp " + Title + "  (" + GrdView.Rows.Count + " items )</a>";

            if (GrdView.Rows.Count == 0)
            {
                //PrDocDowndiv.InnerText= "No available data";
                h4.InnerHtml = "<a class=\"collapsed\">&nbsp &nbsp &nbsp &nbsp " + Title + "  (" + GrdView.Rows.Count + " items )</a>";

                GrdView.Visible = false;
            }
            else
                GrdView.Visible = true;
            

        }
        protected void DataBindDownloadView() 
        {

            grd_General_Partner.DataBind();
            if (grd_General_Partner.Rows.Count != 0)
                grd_General_Partner.Visible = true;
            else
                LblInvestigators.Visible = false;



            grd_General_Infra.DataBind();
            if (grd_General_Infra.Rows.Count != 0)
                grd_General_Infra.Visible = true;
            else
                LblInfrastructure.Visible = false;


            // BreadCrumb.InnerHtml = "You are seeing info about: ";

            DownloadPrDoc_GrdV.DataBind();
            //DownloadPrDoc_GrdV.Columns[9].Visible = false;
            H3PrDocDown.InnerHtml = "<a class=\"expanded\">&nbsp &nbsp &nbsp &nbsp Project Document (" + DownloadPrDoc_GrdV.Rows.Count + " items )</a>";
            if (DownloadPrDoc_GrdV.Rows.Count == 0)
            {
                //PrDocDowndiv.InnerText= "No available data";
                H3PrDocDown.InnerHtml = "<a class=\"collapsed\">&nbsp &nbsp &nbsp &nbsp Project Document (" + DownloadPrDoc_GrdV.Rows.Count + " items )</a>";

                DownloadPrDoc_GrdV.Visible = false;
            }
            else
            {
                DownloadPrDoc_GrdV.Visible = true;
                PrDocDowndiv.InnerText = "";
            }


            DownloadSpecDoc_GrdV.DataBind();
            //DownloadSpecDoc_GrdV.Columns[8].Visible = false;
            H3SpecDocDown.InnerHtml = "<a class=\"expanded\">&nbsp &nbsp &nbsp &nbsp Specimen Document (" + DownloadSpecDoc_GrdV.Rows.Count + "items )</a>";
            if (DownloadSpecDoc_GrdV.Rows.Count == 0)
            {
                H3SpecDocDown.InnerHtml = "<a class=\"collapsed\">&nbsp &nbsp &nbsp &nbsp Specimen Document (" + DownloadSpecDoc_GrdV.Rows.Count + "items )</a>";

                DownloadSpecDoc_GrdV.Visible = false;
                //SpecDocDowndiv.InnerText = "No available data";
            }
            else
            {
                DownloadSpecDoc_GrdV.Visible = true;
                SpecDocDowndiv.InnerText = "";
            }

            DownloadSpecImg_GrdV.DataBind();
            //DownloadSpecImg_GrdV.Columns[4].Visible = false;
            H3SpecImgDown.InnerHtml = "<a class=\"expanded\">&nbsp &nbsp &nbsp &nbsp Specimen Images (" + DownloadSpecImg_GrdV.Rows.Count + "items )</a>";
            if (DownloadSpecImg_GrdV.Rows.Count == 0)
            {
                H3SpecImgDown.InnerHtml = "<a class=\"collapsed\">&nbsp &nbsp &nbsp &nbsp Specimen Images (" + DownloadSpecImg_GrdV.Rows.Count + "items )</a>";

                DownloadSpecImg_GrdV.Visible = false;
                //SpecImgDowndiv.InnerText = "No available data";
            }
            else
            {

                DownloadSpecImg_GrdV.Visible = true;
                SpecImgDowndiv.InnerText = "";
            }

            DownloadMeshModelDoc_GrdV.DataBind();
           // DownloadMeshModelDoc_GrdV.Columns[5].Visible = false;
            H3MeshModelDocDown.InnerHtml = "<a class=\"expanded\">&nbsp &nbsp &nbsp &nbsp Mesh Model Documents ( " + DownloadMeshModelDoc_GrdV.Rows.Count + "items)</a>";
            if (DownloadMeshModelDoc_GrdV.Rows.Count == 0)
            {
                H3MeshModelDocDown.InnerHtml = "<a class=\"collapsed\">&nbsp &nbsp &nbsp &nbsp Mesh Model Documents ( " + DownloadMeshModelDoc_GrdV.Rows.Count + "items)</a>";

                DownloadMeshModelDoc_GrdV.Visible = false;
                //MeshModelDocDowndiv.InnerText = "No available data";
            }
            else
            {

                DownloadMeshModelDoc_GrdV.Visible = true;
                MeshModelDocDowndiv.InnerText = "";
            }


            DownloadMeshModelImg_GrdV.DataBind();
            //DownloadMeshModelImg_GrdV.Columns[3].Visible = false;
            H3MeshModelImgDown.InnerHtml = "<a class=\"expanded\">&nbsp &nbsp &nbsp &nbsp Mesh Model Images ( " + DownloadMeshModelImg_GrdV.Rows.Count + " items)</a>";
            if (DownloadMeshModelImg_GrdV.Rows.Count == 0)
            {
                H3MeshModelImgDown.InnerHtml = "<a class=\"collapsed\">&nbsp &nbsp &nbsp &nbsp Mesh Model Images ( " + DownloadMeshModelImg_GrdV.Rows.Count + " items)</a>";

                DownloadMeshModelImg_GrdV.Visible = false;
                //MeshModelImgDowndiv.InnerText = "No available data";
            }
            else
            {

                DownloadMeshModelImg_GrdV.Visible = true;
                MeshModelImgDowndiv.InnerText = "";
            }


            DownloadExpImg_GrdV.DataBind();
           // DownloadExpImg_GrdV.Columns[2].Visible = false;
            H3ExpImgDown.InnerHtml = "<a class=\"expanded\">&nbsp &nbsp &nbsp &nbsp Experiment Images ( " + DownloadExpImg_GrdV.Rows.Count + "items ) </a>";
            if (DownloadExpImg_GrdV.Rows.Count == 0)
            {
                H3ExpImgDown.InnerHtml = "<a class=\"collapsed\">&nbsp &nbsp &nbsp &nbsp Experiment Images ( " + DownloadExpImg_GrdV.Rows.Count + "items ) </a>";

                DownloadExpImg_GrdV.Visible = false;
                //ExpImgDowndiv.InnerText = "No available data";
            }
            else
            {

                DownloadExpImg_GrdV.Visible = true;
                ExpImgDowndiv.InnerText = "";
            }


            DownloadExpDoc_GrdV.DataBind();
           // DownloadExpDoc_GrdV.Columns[7].Visible = false;
            H3ExpDocDown.InnerHtml = "<a class=\"expanded\">&nbsp &nbsp &nbsp &nbsp Experiment Document ( " + DownloadExpDoc_GrdV.Rows.Count + "items )</a>";
            if (DownloadExpDoc_GrdV.Rows.Count == 0)
            {
                H3ExpDocDown.InnerHtml = "<a class=\"collapsed\">&nbsp &nbsp &nbsp &nbsp Experiment Document ( " + DownloadExpDoc_GrdV.Rows.Count + "items )</a>";

                DownloadExpDoc_GrdV.Visible = false;
                //ExpDocDowndiv.InnerText = "No available data";
            }
            else
            {

                DownloadExpDoc_GrdV.Visible = true;
                ExpDocDowndiv.InnerText = "";
            }

            DownloadExpVid_GrdV.DataBind();
           // DownloadExpVid_GrdV.Columns[2].Visible = false;
            H3ExpVidDown.InnerHtml = "<a class=\"expanded\">&nbsp &nbsp &nbsp &nbsp Experiment Video ( " + DownloadExpVid_GrdV.Rows.Count + "items)</a>";
            if (DownloadExpVid_GrdV.Rows.Count == 0)
            {

                H3ExpVidDown.InnerHtml = "<a class=\"collapsed\">&nbsp &nbsp &nbsp &nbsp Experiment Video ( " + DownloadExpVid_GrdV.Rows.Count + "items)</a>";

                DownloadExpVid_GrdV.Visible = false;
                //ExpVidDowndiv.InnerText = "No available data";
            }
            else
            {

                DownloadExpVid_GrdV.Visible = true;
                ExpVidDowndiv.InnerText = "";
            }

            DownloadSignalResult_GrdV.DataBind();
            //DownloadSignalResult_GrdV.Columns[7].Visible = false;
            H3SignalDown.InnerHtml = "<a class=\"expanded\">&nbsp &nbsp &nbsp &nbsp Output Signal Data (" + DownloadSignalResult_GrdV.Rows.Count + "items ) </a>";
            if (DownloadSignalResult_GrdV.Rows.Count == 0)
            {
                H3SignalDown.InnerHtml = "<a class=\"collapsed\">&nbsp &nbsp &nbsp &nbsp Output Signal Data (" + DownloadSignalResult_GrdV.Rows.Count + "items ) </a>";


                //SignaldDowndiv.InnerText = "No available data";
                DownloadSignalResult_GrdV.Visible = false;
            }
            else
            {
                DownloadSignalResult_GrdV.Visible = true;
                SignaldDowndiv.InnerText = "";
            }


            DownloadOLS_Signal_GrdV.DataBind();
            //DownloadOLS_Signal_GrdV.Columns[7].Visible = false;
            H3OLS_SignalDown.InnerHtml = "<a class=\"expanded\">&nbsp &nbsp &nbsp &nbsp Original Loading Signals Attributes (" + DownloadOLS_Signal_GrdV.Rows.Count + "items ) </a>";
            if (DownloadOLS_Signal_GrdV.Rows.Count == 0)
            {
                H3OLS_SignalDown.InnerHtml = "<a class=\"collapsed\">&nbsp &nbsp &nbsp &nbsp Original Loading Signals Attributes (" + DownloadOLS_Signal_GrdV.Rows.Count + "items ) </a>";


                //SignaldDowndiv.InnerText = "No available data";
                DownloadOLS_Signal_GrdV.Visible = false;
            }
            else
            {
                DownloadOLS_Signal_GrdV.Visible = true;
             
            }




            FindDownloadView();

        }
        protected void FindProjectLevelView() 
        {
            if(GrdViewDetPrData.Rows.Count==0&&GrdViewDetPrDoc.Rows.Count==0&&GrdViewDetPrInfrastr.Rows.Count==0&&GrdViewDetPrInvest.Rows.Count==0)
            {
                MultiView1.ActiveViewIndex = 9;
            }

        }
        protected void FindSpecimenLevelView() 
        {
            if(GridSpecLevelSpecData.Rows.Count==0&&GridSpecLevelSpecDocs.Rows.Count==0&&GridSpecLevelSpecIMGs.Rows.Count==0&&GridSpecLevelSpecScaling.Rows.Count==0&&GridSpecLevelStrElemData.Rows.Count==0&&GridSpecLevelStrElemMatActProp.Rows.Count==0&&GridSpecLevelStrElemMaterial.Rows.Count==0&&GridSpecLevelStrElemMatNomProp.Rows.Count==0)
            {
                MultiView1.ActiveViewIndex = 9;

            }
        }
        protected void FindCompExpLevelView() 
        {
            if (GridCompExpLevelData.Rows.Count == 0 && GridCompExpLevelDLC.Rows.Count == 0 && GridCompExpLevelDOC.Rows.Count == 0 && GridCompExpLevelIMG.Rows.Count == 0 && GridCompExpLevelMeshModel.Rows.Count == 0 && GridCompExpLevelMeshModelDoc.Rows.Count == 0 && GridCompExpLevelMeshModelIMG.Rows.Count == 0 && GridCompExpLevelOLS.Rows.Count == 0 && GridCompExpLevelPC.Rows.Count == 0 && GridCompExpLevelPerson.Rows.Count == 0 && GridCompExpLevelVideo.Rows.Count == 0 && GridCompExpLevelInputFile.Rows.Count == 0 && GridCompExpLevelOLS_Signals.Rows.Count==0)
            {
                MultiView1.ActiveViewIndex = 9;
            }
        }
        protected void FindSignalLevelView() 
        {
            if(GridSignalLevel.Rows.Count==0)
            {
                MultiView1.ActiveViewIndex = 9;
            }
        }
        protected string[] GetLabName() 
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;User Id=root;database=newseriesserver;password=root;");
            MySqlCommand selectLab = new MySqlCommand("select ip, name from Project,laboratory where project.laboratory_idlaboratory = laboratory.idlaboratory and project.idproject = @Prid", conn);

            conn.Open();
            
            selectLab.Parameters.AddWithValue("@Prid", PrIDLabel.Text.Substring("projectid:".Length));
            MySqlDataReader LabReader = selectLab.ExecuteReader();

            string[] labdata = new string [2];

            if (LabReader.Read())
            {
                labdata[0] = LabReader[0].ToString();
                labdata[1] = LabReader[1].ToString();
            }
            conn.Close();
            return labdata;

        }

        //merge the cells
        protected void GridView1_DataBound1(object sender, EventArgs e)
        {



            /*
            GridView SenderReq = (GridView)sender;
            
           
            for (int rowIndex = SenderReq.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = SenderReq.Rows[rowIndex];
                GridViewRow gvPreviousRow = SenderReq.Rows[rowIndex + 1];
               
                //for the download buttons I suppose that if the
                //they have the same local they are very same
                
                if (gvRow.Cells[gvRow.Cells.Count - 2].Text == gvPreviousRow.Cells[gvPreviousRow.Cells.Count - 2].Text)
                {
                    // Set the LinkButton's CommandArgument property with the
                    // row's index.
                    if (gvPreviousRow.Cells[gvRow.Cells.Count - 1].RowSpan < 2)
                    {
                        gvRow.Cells[gvRow.Cells.Count - 1].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[gvRow.Cells.Count - 1].RowSpan = gvPreviousRow.Cells[gvRow.Cells.Count - 1].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[gvRow.Cells.Count - 1].Visible = false;
                }

                gvPreviousRow.Cells[gvRow.Cells.Count - 2].Visible = false;
                gvRow.Cells[gvRow.Cells.Count - 2].Visible = false;
                SenderReq.HeaderRow.Cells[SenderReq.HeaderRow.Cells.Count - 2].Visible = false;
                
                for (int cellCount = 0; cellCount < gvRow.Cells.Count - 2; cellCount++)
                {

                   if (gvRow.Cells[cellCount].Text == gvPreviousRow.Cells[cellCount].Text)
                    {
                        if (gvPreviousRow.Cells[cellCount].RowSpan < 2)
                        {
                            gvRow.Cells[cellCount].RowSpan = 2;
                        }
                        else
                        {
                            gvRow.Cells[cellCount].RowSpan = gvPreviousRow.Cells[cellCount].RowSpan + 1;
                        }
                        gvPreviousRow.Cells[cellCount].Visible = false;
                    }
                }

               
            }



            */
            



        }

        protected string MoveToTheNextState(string state,string PrState) 
        {
            string nextState = null;
            if (PrState.Equals("P") && state.Equals("partner"))
                nextState = "PP";
            if (PrState.Equals("P") && state.Equals("search"))
                nextState = "PS";
            else if (PrState.Equals("PP") && state.Equals("search"))
                nextState = "PSP";
            else if (PrState.Equals("PS") && state.Equals("search"))
                nextState = "PS";
            else if (PrState.Equals("PS") && state.Equals("partner"))
                nextState = "PSP";
            else if (PrState.Equals("PP") && state.Equals("logout"))
                nextState = "P";
            else if (PrState.Equals("PSP") && state.Equals("logout"))
                nextState = "PS";
            else if (PrState.Equals("PSP") && state.Equals("Asearch"))
                nextState = "PSP";
            else if (PrState.Equals("P") && state.Equals("Asearch"))
                nextState = "P";
            else if (PrState.Equals("PP") && state.Equals("Asearch"))
                nextState = "PP";
            else if (PrState.Equals("PS") && state.Equals("Asearch"))
                nextState = "PS";
           
            return nextState;
 
        }
        protected void MovetoTheNextPanelState(string CurrentState,string TriggeredEvent) 
        {



            string xvalue;
            xvalue = Request.Form["x"];


            if(CurrentState==null&&TriggeredEvent.Equals("NotPostback"))
                Session["ActiveView"] = "Home";
            if (xvalue == "" && TriggeredEvent.Equals("LoadData_General"))
                Session["ActiveView"] = "Initial";
            else if (xvalue == "#Download" && TriggeredEvent.Equals("LoadData_General"))
                Session["ActiveView"] = "Download";
            else if (xvalue == "#DetailedInfo" && TriggeredEvent.Equals("LoadData_General"))
                Session["ActiveView"] = "DetailedInfo";
            else if (xvalue == "#GeneralInfo" && TriggeredEvent.Equals("Postback"))
                Session["ActiveView"] = "Initial";
            else if (TriggeredEvent.Equals("Search"))
                Session["ActiveView"] = "Search";
            else if (TriggeredEvent.Equals("Download"))
                Session["ActiveView"] = "Download";
            else if (TriggeredEvent.Equals("Rootnode"))
                Session["ActiveView"] = "Home";
            else if (TriggeredEvent.Equals("LoadData_Lab"))
                Session["ActiveView"] = "LabInfo";
            //else if (xvalue == "" && (TriggeredEvent.Equals("Login") || TriggeredEvent.Equals("Logout")))
              //  Session["ActiveView"] = "Home";
            else if (xvalue == "#Download" && (TriggeredEvent.Equals("Login") || TriggeredEvent.Equals("Logout")))
                Session["ActiveView"] = "Download";
            else if (xvalue == "#DetailedInfo" && (TriggeredEvent.Equals("Login") || TriggeredEvent.Equals("Logout")))
                Session["ActiveView"] = "DetailedInfo";
            else if (TriggeredEvent.Equals("Asearch"))
                Session["ActiveView"] = "Asearch";
           

           // MovetoTheNextPanelState(null, "LoadData_General");
        
        }
        protected string GetDropDownValue() 
        {
            string OrderBySelection = null;
            switch (OrderByList.Text)
            {
                case "Lab":
                    OrderBySelection = "Lab";
                    break;
                case "Title":
                    OrderBySelection = "Title";
                    break;
                case "StartDate":
                    OrderBySelection = "StartDate";
                    break;
                case "OrderBy":
                    OrderBySelection = "Lab";
                    break;

            }
            return OrderBySelection;
        }
        //GridView1_RowCreated1
      
    }


    public class SearchItem : IEquatable<SearchItem>
    {
        public int ProjectID;
        public string ProjectTitle;
        public string Description;
        public string StartDate;
        public string EndDate;
        public string LabName;
        public string LabId;
        public List<string> Level;
        public string ParentId;
        public int Count;

        public SearchItem(string PrTitle, int PrID,string PrDescription,string PrStartDate,string PrEndDate,string PrLabname, string Prlabid, string Prspecimenid , string Prcompexpid,string Prsignalid)
        {
            ProjectID = PrID;
            ProjectTitle = PrTitle;
            Description = PrDescription;
            StartDate = PrStartDate;
            EndDate = PrEndDate;
            LabId = Prlabid;
            LabName = PrLabname;
            Level=new List<string>();
            Count = 1;

            if (Prspecimenid == "") 
            {
                
                Level.Add("project");
                ParentId = PrID.ToString();
            }
            else if (Prcompexpid == "")
            {
                Level.Add("specimen");
                ParentId = Prspecimenid;
            }
            else if (Prsignalid == "")
            {

                Level.Add("computation");
                ParentId = Prcompexpid;
            }
            else if ((Prspecimenid != "") && (Prcompexpid != "") && (Prsignalid != "" ) && (PrID.ToString() != ""))
            {

                Level.Add("signal");
                ParentId = Prsignalid;
            }
        }

        public SearchItem(string PrTitle, int PrID)
        {
            ProjectID = PrID;
            ProjectTitle = PrTitle;
            Description = null;
            StartDate = null;
            EndDate = null;
            LabName = null;
            LabId = null;
            ParentId = null;
            //Level[0] = null;
            Count = 0;

        }
        
        public bool Equals(SearchItem other)
        {
            if (this.ProjectID == other.ProjectID && this.ProjectTitle == other.ProjectTitle )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
