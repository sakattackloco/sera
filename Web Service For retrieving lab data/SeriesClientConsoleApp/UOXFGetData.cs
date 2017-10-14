using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.ServiceModel;
using SeriesClientConsoleApp.ServiceReference1;
using System.Net;

namespace SeriesClientConsoleApp
{
    public class UOXFGetData
    {

        public static DOSClient WSClient ;
        public static DBoperations dbOps = new DBoperations();
        public static Logger CrLogger = new Logger();

        public UOXFGetData(string LabName) 
       { 
            if(LabName.Equals("UOXF"))
                WSClient = new DOSClient("DOSPort1");
            else if (LabName.Equals("JRC"))
                WSClient = new DOSClient("DOSPort2");
            else if (LabName.Equals("UNITN"))
                WSClient = new DOSClient("DOSPort3");
            else if (LabName.Equals("IZIIS"))
                WSClient = new DOSClient("DOSPort4");
            else if (LabName.Equals("NTUA"))
                WSClient = new DOSClient("DOSPort5");
            else if (LabName.Equals("EUCENTRE"))
                WSClient = new DOSClient("DOSPort6");
            else if (LabName.Equals("TUIasi"))
                WSClient = new DOSClient("DOSPort7");
            else if (LabName.Equals("UPAT"))
                WSClient = new DOSClient("DOSPort8");
            else if (LabName.Equals("IFSTTAR"))
                WSClient = new DOSClient("DOSPort9");
            else if (LabName.Equals("ULFGG"))
                WSClient = new DOSClient("DOSPort10");
            else if (LabName.Equals("ITU"))
                WSClient = new DOSClient("DOSPort11");
            else if (LabName.Equals("CELESTEST"))
                WSClient = new DOSClient("DOSPort12");
        }

        static void Main(string[] args)
        {

            UOXFGetData CallUOXFServices = new UOXFGetData("CELESTEST");
            CallUOXFServices.Initialization("CELESTEST");
           

        }


        public void Initialization(string LABname)
        {

         
            // DOSClient WSClient = new DOSClient();

           
            dbOps.OpenConnection();
            string LabID = dbOps.GetLabID(LABname);
            CrLogger.CreateLogIntro("Connection Opened", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), LabID,"Database");
            // string LabID = dbOps.insertLab(LABname, "163.1.8.139", "3", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), "2").ToString();

            dbOps.DeleteInputSignal(LabID);
            CrLogger.CreateLogIntro("delete", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), LabID,"Input Signal");
            
            dbOps.DeleteOLS(LabID);
            CrLogger.CreateLogIntro("delete", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), LabID, "OLS");

            dbOps.DeleteDLC(LabID);
            CrLogger.CreateLogIntro("delete", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), LabID, "DLC");
           
         //   dbOps.DeleteDocument();
         //   CrLogger.CreateLogIntro("delete", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), LabID, "Document");

          //  dbOps.DeleteMaterial();
         //   CrLogger.CreateLogIntro("delete", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), LabID, "Material");
            
           // dbOps.DeletePersonnel();
          //  CrLogger.CreateLogIntro("delete", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), LabID, "Personnel");

            dbOps.DeleteProject(LabID);

            foreach (object x in WSClient.testMe())
            {
                System.Console.WriteLine(x.ToString());
                CrLogger.CreateLogIntro("call", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), LabID, "test");
            }

            foreach (object y in WSClient.testMyKR())
            {
                System.Console.WriteLine(y.ToString());
            }
            /*
            object[] ProjectLocalIDS = WSClient.getProjectIDs();
            foreach (object ProjectLocalID in ProjectLocalIDS)
            {
                string ProjectLocalIDString = ProjectLocalID.ToString();
                //int CurrentInt = Convert.ToInt16(ProjectLocalID);
                project CurrentProject = WSClient.getProjectData(ProjectLocalIDString);
                InsertProjectLevel(LabID, CurrentProject);
                
           

            }
            */

            /*
            string[] ProjectOpenLocalIDS = WSClient.getOpenProjectIDs();
            if (ProjectOpenLocalIDS != null)
            {
                foreach (string ProjectLocalID in ProjectOpenLocalIDS)
                {
                    string ProjectLocalIDString = ProjectLocalID.ToString();
                    //int CurrentInt = Convert.ToInt16(ProjectLocalID);
                    project CurrentProject = WSClient.getProjectData(ProjectLocalIDString);
                    InsertProjectLevel(LabID, CurrentProject);
                }
            }
            */

            string[] ProjectOpenLocalIDS = WSClient.getOpenProjectIDs();
                        
            if (ProjectOpenLocalIDS != null)
            {
                System.Console.WriteLine("1");
                System.Console.WriteLine(ProjectOpenLocalIDS.ToString());
                //System.Console.WriteLine(WSClient.testMe());
                //System.Console.WriteLine(WSClient.testMyKR());


                project cp = WSClient.getProjectData("http://jrc.ec.europa.eu/celestina#project193");
                if (cp != null)
                {
                    System.Console.WriteLine(cp.projectTitle);
                    System.Console.WriteLine(cp.idProject);
                }
                else
                {
                    System.Console.WriteLine("It is null");
              }

                foreach (object x in WSClient.testMe())
                {
                    System.Console.WriteLine(x.ToString());
                }

                foreach (object y in WSClient.testMyKR())
                {
                    System.Console.WriteLine(y.ToString());
                }

                foreach (string ProjectLocalID in ProjectOpenLocalIDS)
                {
                    System.Console.WriteLine("2");
                    string ProjectLocalIDString = ProjectLocalID.ToString();
                    System.Console.WriteLine("Project Local ID String: " + ProjectLocalIDString);
                    //int CurrentInt = Convert.ToInt16(ProjectLocalID);
                    project CurrentProject = WSClient.getProjectData(ProjectLocalIDString);
                    System.Console.WriteLine("Current Project: " + CurrentProject);
                    System.Console.WriteLine("3");
                    InsertProjectLevel(LabID, CurrentProject);
                    System.Console.WriteLine("4");
                }
            }

            string[] ProjectClosedLocalIDS = WSClient.getClosedProjectIDs();
            if (ProjectClosedLocalIDS != null)
            {
                foreach (string ProjectLocalID in ProjectClosedLocalIDS)
                {
                string ProjectLocalIDString = ProjectLocalID.ToString();
                //int CurrentInt = Convert.ToInt16(ProjectLocalID);
                project CurrentProject = WSClient.getProjectData(ProjectLocalIDString);
                InsertProjectLevel(LabID, CurrentProject);
                }
            }

            dbOps.UpdateSearch(); //kanei ena update ton pinaka search

            dbOps.insertLabLastUpdate(DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), LabID); //kanei ena update sto laboratory

            dbOps.CloseConnection();
        }
        public void InsertProjectLevel(string LabID, project CurrentProject)
        {
            string ProjectId;
            string PrInfrastrID;
            string PersonnelID;
            string InstitutionID;


            if (CurrentProject != null)
            {
                System.Console.WriteLine("Project ID: " + CurrentProject.idProject);

                ProjectId = dbOps.InsertProject(LabID, CurrentProject);
                //////////////////////
                dbOps.DeleteSpecimen(ProjectId);

                CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), ProjectId, "project");// kati san istoriko to kanei insert
                infrastructure[] infrastrArray = CurrentProject.infrastructure;
                if (infrastrArray != null)
                {
                    foreach (infrastructure PrInfrastr in infrastrArray)
                    {
                        PrInfrastrID = dbOps.InsertInfrastructure(PrInfrastr);
                        CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), PrInfrastrID, "PrInfrastructure");
                        dbOps.InsertProjectHasLocation(PrInfrastrID, ProjectId);
                        CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), ProjectId, "PrHasLocation");
                    }
                }
                ////////////////
                person[] PersonArray = CurrentProject.projectPerson;
                if (PersonArray != null)
                {
                    foreach (person ProjectPerson in PersonArray)
                    {


                        //check if Institution exists and insert it if not
                        //get the id and insert it in the personnel table as FK
                        institution PersonInstitution = ProjectPerson.institution;

                        InstitutionID = dbOps.InsertInstitution(PersonInstitution);
                        CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), InstitutionID, "Institution");
                        PersonnelID = dbOps.InsertPersonnel(ProjectPerson, InstitutionID);
                        if (dbOps.CheckIfproject_has_personnelExists(ProjectId, PersonnelID) == null)
                        {
                            dbOps.InsertProjectHasPersonnel(ProjectId, PersonnelID, ProjectPerson.role);
                            CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), ProjectId, "PrHasPersonnel");
                        }
                    }
                }
                string DocId;
                document[] PrDocs = WSClient.getProjectDocuments(CurrentProject.idProject);
                if (PrDocs != null)
                {
                    foreach (document PrDoc in PrDocs)
                    {
                        DocId = dbOps.InsertProjectReport(ProjectId, PrDoc);
                        CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), DocId, "Document");
                    }
                }

                specimen RunningSpecimen;

                int?[] SpecimenIDs = CurrentProject.specimenIDs;
                if (SpecimenIDs != null)
                {
                    for (int i = 0; i < SpecimenIDs.Length; i++)
                    {
                        //RunningSpecimen = WSClient.getSpecimenData((int)SpecimenIDs[i]);
                        RunningSpecimen = WSClient.getSpecimenData(SpecimenIDs[i].ToString());

                        InsertSpecimenLevel(ProjectId, RunningSpecimen);

                    }//end of specimen array

                }
                ////////////
                
            }

        }
        public void InsertSpecimenLevel(string ProjectId, specimen RunningSpecimen)
        {

            string SpecimenID;
            string MateriaID;
            scaling[] RunningScaling;
            string idStructuralElement;
            material[] MaterialArray;
            actualMeanProperty[] ActualMeanPropArray;
            nominalProperty[] NominalPropertyArray;
            document[] StrElementDocArray;
            document[] MaterialDocArray;
            experiment RunningExp;
            computation RunningComp;



            SpecimenID = dbOps.InsertSpecimen(ProjectId, RunningSpecimen);
            CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), SpecimenID, "Specimen");
            string SimilitudeId;

            //Get the scalation data and insert them in db
            RunningScaling = RunningSpecimen.scalingList;
            if (RunningScaling != null)
            {

                foreach (scaling scalation in RunningScaling)
                {

                   SimilitudeId = dbOps.InsertSimilitude(scalation, SpecimenID);
                   CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), SimilitudeId, "Similitude");
                }
            }


            string SpecimenReportId;    
            //get the specimen documents and insert them
            document[] SpecDocs = WSClient.getSpecimenDocuments(RunningSpecimen.idSpecimen);
            if (SpecDocs != null)
            {
                foreach (document SpecDoc in SpecDocs)
                {
                    SpecimenReportId = dbOps.InsertSpecimenReport(SpecimenID, SpecDoc);
                    CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), SpecimenReportId, "SpecimenReport");

                }
            }

            //Get the structural element of the specimen
            structuralComponent[] SpecStrElementArray = WSClient.getStructuralComponentData(RunningSpecimen.idSpecimen);
            string StructElemDocID = null;
            if (SpecStrElementArray != null)
            {
                foreach (structuralComponent SpecStrElement in SpecStrElementArray)
                {

                    idStructuralElement = dbOps.InsertStructuralElement(SpecimenID, SpecStrElement);
                    CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), idStructuralElement, "Structural Element");

                    StrElementDocArray = SpecStrElement.structuralComponentDocumentList;
                    if (StrElementDocArray != null)
                    {
                        foreach (document StrElemenetDoc in StrElementDocArray)
                        {
                            dbOps.InsertStrElementDoc(idStructuralElement, StrElemenetDoc);
                            CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), StructElemDocID, "Structural Element Document");
                        }
                    }


                    MaterialArray = SpecStrElement.structuralComponentMaterialList;
                    string MaterialDocId;

                    if (MaterialArray != null)
                    {
                        foreach (material StrElementMaterial in MaterialArray)
                        {
                            MateriaID = dbOps.InsertMaterial(StrElementMaterial);
                            CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), MateriaID, "Material");

                            MaterialDocArray = StrElementMaterial.materialDocumentList;
                            if (MaterialDocArray != null)
                            {
                                foreach (document materialDoc in MaterialDocArray)
                                {
                                    MaterialDocId = dbOps.InsertMaterialDoc(MateriaID, idStructuralElement, materialDoc);
                                    CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), MaterialDocId, "Material Document");
                                }
                            }

                            string MaterialNominalPropertyID;
                            NominalPropertyArray = StrElementMaterial.nominalPropertyList;
                            if (NominalPropertyArray != null)
                            {
                                foreach (nominalProperty NoninalProp in NominalPropertyArray)
                                {

                                    if (NoninalProp != null)
                                    {
                                        MaterialNominalPropertyID = dbOps.InsertMaterialNominalProperty(MateriaID, NoninalProp);
                                        CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), MaterialNominalPropertyID, "Material Nominal Property");
                                    }
                                    //NoninalProp.nominalPropertyDocumentList;
                                    //NoninalProp.valueVectorX;
                                    //NoninalProp.valueVectorY;
                                }
                            }

                            ActualMeanPropArray = StrElementMaterial.actualMeanPropertyList;
                            string MaterialActualPropertiesID=null;
                            if (ActualMeanPropArray != null)
                            {
                                foreach (actualMeanProperty ActualProp in ActualMeanPropArray)
                                {

                                    if (ActualProp != null)
                                    {
                                        dbOps.InsertMaterialActualProperties(idStructuralElement, MateriaID, ActualProp);
                                        CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), MaterialActualPropertiesID, "Material Actual Property");
                                    }
                                        
                                    //ActualProp.actualMeanPropertyDocumentList;
                                    //ActualProp.valueVectorX;
                                    //ActualProp.valueVectorY;
                                }
                            }



                            dbOps.InsertStrElementHasMaterial(SpecimenID, idStructuralElement, MateriaID);
                            CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), SpecimenID, "Structural Element Has Material");

                        }//end of material array
                    }

                }//end of structural component array
            }

            //get and insert the specimen images
            image[] SpecimenImagesArray = WSClient.getSpecimenImages(RunningSpecimen.idSpecimen);
            string SpecimenPictureID=null;
            if (SpecimenImagesArray != null)
            {
                foreach (image CurrentImg in SpecimenImagesArray)
                {
                    dbOps.InsertSpecimenPicture(SpecimenID, CurrentImg);
                    CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), SpecimenPictureID, "Specimen Picture");
                }
            }

            //Start Getting Experiments 
            if (RunningSpecimen.experimentIDs != null)
            {
                for (int ExpCount = 0; ExpCount < RunningSpecimen.experimentIDs.Length; ExpCount++)
                {
                    System.Console.WriteLine("Running specimen: " + RunningSpecimen.experimentIDs[ExpCount].ToString());
                    RunningExp = WSClient.getExperimentData(RunningSpecimen.experimentIDs[ExpCount].ToString());
                    InsertExpLevel(SpecimenID, RunningExp);

                }//end of experiment
            }


            //Start Inserting the Computations
            if (RunningSpecimen.computationIDs != null)
            {

                for (int CompCount = 0; CompCount < RunningSpecimen.computationIDs.Length; CompCount++)
                {
                    //experiment test = WSClient.getExperimentData(2);
                    RunningComp = WSClient.getComputationData(RunningSpecimen.computationIDs[CompCount].ToString());
                    if (RunningComp != null)
                    {
                        InsertComputationLevel(SpecimenID, RunningComp);

                    }
                }//end of Computation

            }

        }
        public void InsertComputationLevel(string SpecimenID, computation RunningComp)
        {


            string PersonnelID;
            string InstitutionID;

            string MateriaID;
            string ExpCompID;
            string MeshModelID;


            person[] ExpCompPersonArray;


            image[] CompExpImagesArray;
            document[] CompExpDocsArray;
            video[] CompExpVideosArray;
            string IDDocument;
            detLoadChar[] DLCArray;

            signal[] OutputSignalArray;
            meshModel[] MeshModelArray;
            document[] MeshModelDocArray;
            image[] MeshModelImgArray;
            material[] MeshModelMaterialArray;
            string DLCH_ID = null;
            originalLoadingSignal[] OLSArray;
            signal[] EffectiveInputArray;
            string EffectiveInputFileID;
            string OlsID;

            ExpCompID = dbOps.InsertCompExp(SpecimenID, RunningComp);
            CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), ExpCompID, "ExpComp");
            //dbOps.InsertComputerSystem(ExpCompID, RunningComp.computerSystem);

            MeshModelArray = RunningComp.meshModelCollection;
            string MeshModel_Has_Materialid = null;
            if (MeshModelArray != null)
            {
                foreach (meshModel RunningMeshModel in MeshModelArray)
                {


                    MeshModelID = dbOps.InsertMeshModel(RunningMeshModel, ExpCompID);
                    CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), MeshModelID, "Mesh Model");

                    MeshModelMaterialArray = RunningMeshModel.meshModelMaterialList;
                    if (MeshModelMaterialArray != null)
                    {
                        foreach (material MeshModelMaterial in MeshModelMaterialArray)
                        {
                            MateriaID = dbOps.InsertMaterial(MeshModelMaterial);
                            CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), MateriaID, "Material");

                            dbOps.InsertMeshModel_Has_Material(MateriaID, MeshModelID);
                            CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), MeshModel_Has_Materialid, "MeshModel Has Material");
                        }

                    }



                    //Mesh Model Documents
                    MeshModelDocArray = RunningMeshModel.meshModelDocumentList;
                    string MeshModelDocID = null;
                    if (MeshModelDocArray != null)
                    {
                        foreach (document MeshModelDoc in MeshModelDocArray)
                        {
                            MeshModelDocID = dbOps.InsertMeshModelDoc(MeshModelDoc, MeshModelID);
                            CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), MeshModelDocID, "Mesh Model Doc");
                        }
                    }
                    string MeshModelImageID = null;
                    MeshModelImgArray = RunningMeshModel.meshModelImageList;
                    if (MeshModelImgArray != null)
                    {
                        foreach (image RunningMeshModelImg in MeshModelImgArray)
                        {
                            MeshModelImageID = dbOps.InsertMeshModelPic(RunningMeshModelImg, MeshModelID);
                            CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), MeshModelImageID, "Mesh Model Image");
                        }
                    }



                }
            }

            ExpCompPersonArray = RunningComp.experimentComputationPerson;
            string CompHasPersonnelID = null;
            if (ExpCompPersonArray != null)
            {
                foreach (person ExpCompPerson in ExpCompPersonArray)
                {
                    institution PersonInstitution = ExpCompPerson.institution;

                    InstitutionID = dbOps.InsertInstitution(ExpCompPerson.institution);
                    CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), InstitutionID, "Institution");

                    PersonnelID = dbOps.InsertPersonnel(ExpCompPerson, InstitutionID);
                    CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), PersonnelID, "Personnel");

                    if (dbOps.CheckIfCompHasPersonnelExist(ExpCompID, PersonnelID) == null)
                    {
                        dbOps.InsertCompExpHasPersonnel(ExpCompID, PersonnelID);
                        CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), CompHasPersonnelID, "Comp Has Personnel");
                    }

                }//end of experiment person

            }

            CompExpDocsArray = WSClient.getComputationDocuments(RunningComp.idExpComp);
            string CompExpHasDocID = null;
            if (CompExpDocsArray != null)
            {
                foreach (document CompExpDoc in CompExpDocsArray)
                {
                    IDDocument = dbOps.InsertDocument(CompExpDoc);
                    CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), IDDocument, "Computation Document");

                    CompExpHasDocID = dbOps.InsertCompExpHasDoc(ExpCompID, IDDocument);
                    CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), CompExpHasDocID, "Computation Has Document");

                }
            }

            CompExpImagesArray = WSClient.getComputationImages(RunningComp.idExpComp);
            string CompExpPicID = null;
            if (CompExpImagesArray != null)
            {
                foreach (image CompExpImage in CompExpImagesArray)
                {
                    CompExpPicID = dbOps.InsertCompExpPictures(ExpCompID, CompExpImage);
                    CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), CompExpPicID, "Computation Picture");
                }

            }

            CompExpVideosArray = WSClient.getComputationVideos(RunningComp.idExpComp);
            string VideoID = null;
            if (CompExpVideosArray != null)
            {
                foreach (video CompExpVideo in CompExpVideosArray)
                {
                    VideoID = dbOps.InsertVideo(ExpCompID, CompExpVideo);
                    CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), VideoID, "Video");
                }
            }



            //Detailed Loading Characteristics comp
            DLCArray = WSClient.getComputationLoadingData(RunningComp.idExpComp);
            string CompExpHasDLCID = null;
            string NominalLoading_OriginalLoadingID = null;
            if (DLCArray != null)
            {
                foreach (detLoadChar dlc in DLCArray)
                {
                    OLSArray = dlc.listOriginalLoadingSignals;
                    
                    DLCH_ID = dbOps.InsertDLC(dlc);
                    CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), DLCH_ID, "DLCH");

                   CompExpHasDLCID= dbOps.InsertCompExp_has_dlc(DLCH_ID, ExpCompID);
                   CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), CompExpHasDLCID, "CompExpHasDLC");

                    if (OLSArray != null)
                    {
                        for (int index = 0; index < OLSArray.Length; index++)
                        {

                            EffectiveInputFileID = dbOps.InsertInputSignal(OLSArray[index].signal);
                            CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), EffectiveInputFileID, "EffectiveInputFile");

                            OlsID = dbOps.InsertOriginalLoading(OLSArray[index], EffectiveInputFileID);
                            CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), OlsID, "Ols");

                            //EffectiveInputFileID = dbOps.InsertInputSignal(EffectiveInputArray[index]);
                            NominalLoading_OriginalLoadingID = dbOps.InsertNominalLoading_OriginalLoading(OlsID, DLCH_ID, OLSArray[index].direction, EffectiveInputFileID);
                            CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), NominalLoading_OriginalLoadingID, "NominalLoading_OriginalLoading");
                        }
                    }
                    //signals
                }
            }

            string ComputerSystemID = null;
            OutputSignalArray = RunningComp.outputSignals;
            if (OutputSignalArray != null)
            {
                foreach (signal OutputSignal in OutputSignalArray)
                {

                    InsertSignalLevel(ExpCompID, OutputSignal);
                }
            }
            if (RunningComp.computerSystem != null)
            {
                ComputerSystemID = dbOps.InsertComputerSystem(ExpCompID, RunningComp.computerSystem);
                CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), ComputerSystemID, "ComputerSystem");
            }
        }
        public void InsertExpLevel(string SpecimenID, experiment RunningExp)
        {


            string PersonnelID;
            string InstitutionID;
            string ExpCompID;
            person[] ExpCompPersonArray;
            image[] CompExpImagesArray;
            document[] CompExpDocsArray;
            video[] CompExpVideosArray;
            string IDDocument;
            detLoadChar[] DLCArray;
            signal[] OutputSignalArray;
            string DLCH_ID = null;
            originalLoadingSignal[] OLSArray;
            signal[] EffectiveInputArray;
            string EffectiveInputFileID;
            string OlsID;


            ExpCompID = dbOps.InsertCompExp(SpecimenID, RunningExp);
            CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), ExpCompID, "CompExp");
            string CompExpHasPersonnelID=null;
    
            ExpCompPersonArray = RunningExp.experimentComputationPerson;
            if (ExpCompPersonArray != null)
            {
                foreach (person ExpCompPerson in ExpCompPersonArray)
                {
                    institution PersonInstitution = ExpCompPerson.institution;
                    InstitutionID = dbOps.InsertInstitution(ExpCompPerson.institution);
                    CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), InstitutionID, "Institution");
                    PersonnelID = dbOps.InsertPersonnel(ExpCompPerson, InstitutionID);
                    CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), PersonnelID, "Personnel");

                    if (dbOps.CheckIfCompHasPersonnelExist(ExpCompID, PersonnelID) == null)
                    {
                        CompExpHasPersonnelID = dbOps.InsertCompExpHasPersonnel(ExpCompID, PersonnelID).ToString();
                        CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), CompExpHasPersonnelID, "CompExpHasPersonnel");
                    }

                }//end of experiment person

            }
            //RunningExp.outputSignals;
            CompExpDocsArray = WSClient.getExperimentDocuments(RunningExp.idExpComp);
            string CompExpHasDocumentId=null;
            if (CompExpDocsArray != null)
            {
                foreach (document CompExpDoc in CompExpDocsArray)
                {
                    IDDocument = dbOps.InsertDocument(CompExpDoc);
                    CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), IDDocument, "CompExpDocument");
                    CompExpHasDocumentId = dbOps.InsertCompExpHasDoc(ExpCompID, IDDocument);
                    CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), CompExpHasDocumentId, "CompExpHasDocument");

                }
            }
            string CompExpImagesID=null;
            CompExpImagesArray = WSClient.getExperimentImages(RunningExp.idExpComp);
            if (CompExpImagesArray != null)
            {
                foreach (image CompExpImage in CompExpImagesArray)
                {

                    dbOps.InsertCompExpPictures(ExpCompID, CompExpImage);
                    CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), CompExpImagesID, "CompExpImages");
                }

            }

            CompExpVideosArray = WSClient.getExperimentVideos(RunningExp.idExpComp);
            string CompExpVideoID=null;
            if (CompExpVideosArray != null)
            {
                foreach (video CompExpVideo in CompExpVideosArray)
                {
                    dbOps.InsertVideo(ExpCompID, CompExpVideo);
                    CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), CompExpVideoID, "CompExpVideo");
                }
            }

            //Detailed Loading Characteristics exp
            DLCArray = WSClient.getExperimentLoadingData(RunningExp.idExpComp);
            string NominalLoadingOlsID;
            string CompExpHasDLCHID;
            if (DLCArray != null)
            {
                foreach (detLoadChar dlc in DLCArray)
                {

                    OLSArray = dlc.listOriginalLoadingSignals;
                   
                    DLCH_ID = dbOps.InsertDLC(dlc);
                    CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), DLCH_ID, "DLCH");

                    CompExpHasDLCHID = dbOps.InsertCompExp_has_dlc(DLCH_ID, ExpCompID);
                    CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), CompExpHasDLCHID, "CompExpHasDLCH");

                    if (OLSArray != null)
                    { 
                        for (int index = 0; index < OLSArray.Length; index++)
                        {

                            EffectiveInputFileID = dbOps.InsertInputSignal(OLSArray[index].signal);
                            CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), EffectiveInputFileID, "Effective Input File");
                            
                            OlsID = dbOps.InsertOriginalLoading(OLSArray[index], EffectiveInputFileID);
                            CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), OlsID, "OLS");

                            NominalLoadingOlsID = dbOps.InsertNominalLoading_OriginalLoading(OlsID, DLCH_ID, OLSArray[index].direction, EffectiveInputFileID);
                            CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), NominalLoadingOlsID, "Nominal Loading Original Loading");

                        }
                    }
                    //signals
                }
            }

       
            OutputSignalArray = RunningExp.outputSignals;
            if (OutputSignalArray != null)
            {
                foreach (signal OutputSignal in OutputSignalArray)
                {
                    //to kanw insert parakatw
                    InsertSignalLevel(ExpCompID, OutputSignal);
                    


                }
            }
        }
        public void InsertSignalLevel(string ExpCompID, signal OutputSignal)
        {
            string OutputSignalID = dbOps.InsertOutputSignal(ExpCompID, OutputSignal);
            CrLogger.CreateLogIntro("insert", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), OutputSignalID, "Output Signal");
        }

     


        
    }




    public class DBoperations
    {
        private MySqlConnection conn = new MySqlConnection();
        private string connectionStr = "server=localhost;user=root;database=newseriesserver;port=3306;password=root;";

        public string OpenConnection()
        {
            conn.ConnectionString = connectionStr;
            conn.Open();
            return conn.State.ToString();
        }

        public string CloseConnection()
        {
            conn.Close();
            return conn.State.ToString();
        }

        public void testConn()
        {
            System.Console.WriteLine(conn.State);
        }

        public string GetLabID(string name)
        {
            string sqlSelect = "select idlaboratory from laboratory where name = '" + name + "'" ;
            MySqlCommand select = new MySqlCommand(sqlSelect, conn);
            MySqlDataReader data = select.ExecuteReader();
            string result = null;
            while (data.Read())
            {
                //System.Console.WriteLine(data[0]);
                //System.Console.WriteLine((data[0].ToString() == name));
                if (data[0] != null)
                {
                    System.Console.WriteLine("the selected name exists");
                    result = data[0].ToString();
                }
                else
                {
                    System.Console.WriteLine("the selected name DOES NOT exists");
                    result = null;
                }
            }
            data.Close();
            return result;
        }

        public string insertLab(string name, string ip, string interval, string LastUpdateTime, string TimeDif)
        {
            string sqlInsert = "Insert Into laboratory" + " (Name, ip, Interv,LastUpdateTime,TimeDif) Values" + " (@Name,@ip,@interval,@LastUpdateTime,@TimeDif) ";
            MySqlCommand insert = new MySqlCommand(sqlInsert, conn);
            insert.Parameters.AddWithValue("@Name", name);
            insert.Parameters.AddWithValue("@ip", ip);
            insert.Parameters.AddWithValue("@interval", interval);
            insert.Parameters.AddWithValue("@LastUpdateTime", LastUpdateTime);
            insert.Parameters.AddWithValue("@TimeDif", TimeDif);
            insert.ExecuteNonQuery();

            return insert.LastInsertedId.ToString();
        }

        public string insertLabLastUpdate(string LastUpdateTime ,string labid)
        {
            string sqlInsert = "Update laboratory set " + " LastUpdateTime =" + " @LastUpdateTime  where idlaboratory = @idlab";
            MySqlCommand insert = new MySqlCommand(sqlInsert, conn);
            insert.Parameters.AddWithValue("@LastUpdateTime", LastUpdateTime);
            insert.Parameters.AddWithValue("@idlab", labid);
            insert.ExecuteNonQuery();

            return insert.LastInsertedId.ToString();
        }





        public string InsertProject(string idLab, project NewProject)
        {
            string PrID = CheckIFprojectExists((int.Parse(NewProject.idProject)), idLab);

            string reasonOfPr;
            if (NewProject.projectDescription == null)
                reasonOfPr = "No description available";
            else
                reasonOfPr = NewProject.projectDescription;


            if (PrID == null)
            {
                string SqlInserPr = "insert into Project" + " (laboratory_idlaboratory, Title, StartDate, EndDate, Reason, Acronym, Sponsor, LocalId,privacy,MainFocus,DateCreated) values " + " (@laboratory_idlaboratory,@Title,@StartDate,@EndDate,@Reason,@Acronym,@Sponsor,@LocalId,@privacy,@MainFocus, @DateCreated)";
                MySqlCommand insertProj = new MySqlCommand(SqlInserPr, conn);
                insertProj.Parameters.AddWithValue("@laboratory_idlaboratory", idLab);
                insertProj.Parameters.AddWithValue("@title", NewProject.projectTitle);
                insertProj.Parameters.AddWithValue("@StartDate", NewProject.projectStartDate);
                insertProj.Parameters.AddWithValue("@EndDate", NewProject.projectEndDate);
                insertProj.Parameters.AddWithValue("@reason", reasonOfPr);
                insertProj.Parameters.AddWithValue("@localID", NewProject.idProject);
                insertProj.Parameters.AddWithValue("@acronym", NewProject.acronym);
                insertProj.Parameters.AddWithValue("@status", NewProject.projectStatus);
                insertProj.Parameters.AddWithValue("@sponsor", NewProject.fundingOrganization);
                insertProj.Parameters.AddWithValue("@MainFocus", NewProject.projectMainFocus);
                insertProj.Parameters.AddWithValue("@privacy", NewProject.privacy);
                insertProj.Parameters.AddWithValue("@DateCreated", DateTime.Now);

                insertProj.ExecuteNonQuery();
                PrID = insertProj.LastInsertedId.ToString();

            }
            else 
            {
                string SqlInserPr = "Update Project set " + " Title= @Title, StartDate=@StartDate, EndDate=@EndDate, Reason=@Reason, Acronym=@Acronym, Sponsor=@Sponsor,privacy=@privacy,MainFocus=@MainFocus where localid= @localid and laboratory_idlaboratory = @idlaboratory ";
                MySqlCommand insertProj = new MySqlCommand(SqlInserPr, conn);
             
                insertProj.Parameters.AddWithValue("@title", NewProject.projectTitle);
                insertProj.Parameters.AddWithValue("@StartDate", NewProject.projectStartDate);
                insertProj.Parameters.AddWithValue("@EndDate", NewProject.projectEndDate);
                insertProj.Parameters.AddWithValue("@reason", NewProject.projectDescription);
                insertProj.Parameters.AddWithValue("@acronym", NewProject.acronym);
                insertProj.Parameters.AddWithValue("@status", NewProject.projectStatus);
                insertProj.Parameters.AddWithValue("@sponsor", NewProject.fundingOrganization);
                insertProj.Parameters.AddWithValue("@MainFocus", NewProject.projectMainFocus);
                insertProj.Parameters.AddWithValue("@privacy", NewProject.privacy);

                insertProj.Parameters.AddWithValue("@localid", NewProject.idProject);
                insertProj.Parameters.AddWithValue("@idlaboratory", idLab);

                insertProj.ExecuteNonQuery();
                
                
            }
                

            return PrID;
        }
        public string CheckIFprojectExists(int ProjectLocalID, string idlab)
        {
            string projectId = null;
            string sqlSelectPr = "select idProject from Project where project.Localid = @localId AND project.laboratory_idlaboratory = @idlab";
            MySqlCommand selectPr = new MySqlCommand(sqlSelectPr, conn);
            selectPr.Parameters.AddWithValue("@localId", ProjectLocalID);
            selectPr.Parameters.AddWithValue("@idlab", idlab);
            MySqlDataReader projects = selectPr.ExecuteReader();
            if (projects.HasRows)
            {
                projects.Read();
                projectId = projects[0].ToString();
            }
            projects.Close();
            return projectId;
        }

        public string InsertInfrastructure(infrastructure infrastru)
        {
            //System.Console.WriteLine(infrastru.resourceName + infrastru.location + infrastru.infrastructureName);

            string LocationId = CheckIfLocationExists(infrastru);
            //first we check if the location exists and then we insert it
            //if the location exists the function returns the id,so we can
            //check if the project-location exists
            if (LocationId == null)
            {
                string SqlInsertLocation = "insert into Location" + " (Infrastructure,Resource,location) values " + " (@Infrastructure,@Resource,@location)";
                MySqlCommand insertLocation = new MySqlCommand(SqlInsertLocation, conn);
                insertLocation.Parameters.AddWithValue("@Infrastructure", infrastru.infrastructureName);
                insertLocation.Parameters.AddWithValue("@Resource", infrastru.facilityName);
                insertLocation.Parameters.AddWithValue("@location", infrastru.location);
                //insertLocation.Parameters.AddWithValue("@LocalId", infrastru.idInfrastructure);
                insertLocation.ExecuteNonQuery();
                LocationId = insertLocation.LastInsertedId.ToString();
            }
            return LocationId;
        }

        //the old check if location exists had also labID?????
        public string CheckIfLocationExists(infrastructure infrastru)
        {
            string locationId = null;
            string sqlSelectLocation = "select location.idlocation from location where Infrastructure =@Infrastructure  and Resource=@Resource and location =@location ";
            MySqlCommand selectLocation = new MySqlCommand(sqlSelectLocation, conn);

            //selectLocation.Parameters.AddWithValue("@localid", LocalId);
            selectLocation.Parameters.AddWithValue("@Infrastructure", infrastru.infrastructureName);
            selectLocation.Parameters.AddWithValue("@Resource", infrastru.facilityName);
            selectLocation.Parameters.AddWithValue("@location", infrastru.location);


            MySqlDataReader locationReader = selectLocation.ExecuteReader();
            if (locationReader.HasRows)
            {
                locationReader.Read();
                locationId = locationReader[0].ToString();
            }
            locationReader.Close();
            return locationId;

        }
        public string CheckIfProjHasLocationExists(string ProjectID, string LocationID)
        {
            string SqlselectLocation = "select Location_idLocation from project_has_location where project_has_location.Project_idProject=@ProjectId AND project_has_location.Location_idLocation=@location";
            string locationId = null;
            MySqlCommand SelectLocationID = new MySqlCommand(SqlselectLocation, conn);
            SelectLocationID.Parameters.AddWithValue("@location", LocationID);
            SelectLocationID.Parameters.AddWithValue("@projectID", ProjectID);
            MySqlDataReader LocationReader = SelectLocationID.ExecuteReader();
            if (LocationReader.HasRows)
            {
                LocationReader.Read();
                locationId = LocationReader[0].ToString();
            }
            LocationReader.Close();
            return locationId;
        }
        public string InsertProjectHasLocation(string LocationId, string ProjectId)
        {
            string result = null;
            if (CheckIfProjHasLocationExists(ProjectId, LocationId) == null)
            {
                string SqlProjectHasLocation = "insert into Project_has_Location" + " (Location_idLocation,Project_idProject) values " + " (@LocationId, @ProjectId)";
                MySqlCommand ProjectHasLocation = new MySqlCommand(SqlProjectHasLocation, conn);
                ProjectHasLocation.Parameters.AddWithValue("@LocationId", LocationId);
                ProjectHasLocation.Parameters.AddWithValue("@ProjectId", ProjectId);
                result = ProjectHasLocation.ExecuteNonQuery().ToString();
            }
            return result;
        }

        public string InsertPersonnel(person personnel, string InstitutionID)
        {

            string IdPersonnel = CheckIFPersonnelExists(personnel, InstitutionID);

            if (IdPersonnel == null)
            {
                System.Console.WriteLine("person does not exist");
                string SqlInsertPersonnel = "insert into Personnel" + " (Name,Institution_idInstitution,localid) values " + " (@Name,@Institution,@localid)";
                MySqlCommand InsertPersonnel = new MySqlCommand(SqlInsertPersonnel, conn);
                InsertPersonnel.Parameters.AddWithValue("@Name", personnel.familyName);
                InsertPersonnel.Parameters.AddWithValue("@Institution", InstitutionID);
                InsertPersonnel.Parameters.AddWithValue("@localid", personnel.idPerson);
                InsertPersonnel.ExecuteNonQuery();
                IdPersonnel = InsertPersonnel.LastInsertedId.ToString();
            }

            return IdPersonnel;
        }
        public string CheckIFPersonnelExists(person Personnel, string InstitutionID)
        {
            string SqlselectPerson = "select idPersonnel from personnel where LocalId = @LocalId AND Institution_idInstitution = @Institution AND Name = @Name";
            string IdPersonnel = null;
            MySqlCommand SelectPersonnel = new MySqlCommand(SqlselectPerson, conn);
            SelectPersonnel.Parameters.AddWithValue("@LocalId", Personnel.idPerson);
            SelectPersonnel.Parameters.AddWithValue("@Institution", InstitutionID);
            SelectPersonnel.Parameters.AddWithValue("@Name", Personnel.familyName);
            MySqlDataReader PersonnelReader = SelectPersonnel.ExecuteReader();
            if (PersonnelReader.HasRows)
            {
                PersonnelReader.Read();
                IdPersonnel = PersonnelReader[0].ToString();
            }
            PersonnelReader.Close();

            return IdPersonnel;
        }
        public string CheckIfproject_has_personnelExists(string idProject, string idPersonnel)
        {
            string SqlselectPrPerson = "select Personnel_idPersonnel from project_has_personnel where Project_idProject = @idProject AND Personnel_idPersonnel = @idPersonnel ";
            string IdPrPerson = null;
            MySqlCommand selectPrPerson = new MySqlCommand(SqlselectPrPerson, conn);
            selectPrPerson.Parameters.AddWithValue("@idProject", idProject);
            selectPrPerson.Parameters.AddWithValue("@idPersonnel", idPersonnel);
            MySqlDataReader PrPerson = selectPrPerson.ExecuteReader();
            if (PrPerson.HasRows)
            {
                PrPerson.Read();
                IdPrPerson = PrPerson[0].ToString();
            }
            PrPerson.Close();

            return IdPrPerson;

        }
        public string InsertProjectHasPersonnel(string ProjectId, string PersonnelId, string role)
        {
            string IdPersonnel = null;
            string SqlInsertProjectHasPersonnel = "insert into Project_has_Personnel" + " (Project_idProject,Personnel_idPersonnel,role) values " + " (@Project_idProject,@Personnel_idPersonnel,@role)";
            MySqlCommand InsertProjectHasPersonnel = new MySqlCommand(SqlInsertProjectHasPersonnel, conn);
            InsertProjectHasPersonnel.Parameters.AddWithValue("@Project_idProject", ProjectId);
            InsertProjectHasPersonnel.Parameters.AddWithValue("@Personnel_idPersonnel", PersonnelId);
            InsertProjectHasPersonnel.Parameters.AddWithValue("@role", role);
            IdPersonnel = InsertProjectHasPersonnel.ExecuteNonQuery().ToString();

            return IdPersonnel;

        }


        public string InsertInstitution(institution Instit)
        {
            //System.Console.WriteLine(infrastru.resourceName + infrastru.location + infrastru.infrastructureName);

            string InstititutionId = CheckIfInstitutionExists(Instit);
            //first we check if the location exists and then we insert it
            //if the location exists the function returns the id,so we can
            //check if the project-location exists
            if (InstititutionId == null)
            {
                string SqlInsertInstititution = "insert into institution" + " (Acronym,Name) values " + " (@Acronym,@Name)";
                MySqlCommand insertInstititution = new MySqlCommand(SqlInsertInstititution, conn);
                insertInstititution.Parameters.AddWithValue("@Acronym", Instit.institutionAcronym);
                insertInstititution.Parameters.AddWithValue("@Name", Instit.institutionName);

                //insertLocation.Parameters.AddWithValue("@LocalId", infrastru.idInfrastructure);

                insertInstititution.ExecuteNonQuery();
                InstititutionId = insertInstititution.LastInsertedId.ToString();
            }
            return InstititutionId;
        }
        public string CheckIfInstitutionExists(institution Instit)
        {

            string SqlselectIdInstitution = "select idInstitution from Institution where (Acronym = @Acronym or Acronym is null ) and Name = @Name";
            string IdInstitution = null;
            MySqlCommand selectInstitution = new MySqlCommand(SqlselectIdInstitution, conn);
            selectInstitution.Parameters.AddWithValue("@Acronym", Instit.institutionAcronym);
            selectInstitution.Parameters.AddWithValue("@Name", Instit.institutionName);
            MySqlDataReader InstitutionReader = selectInstitution.ExecuteReader();
            if (InstitutionReader.HasRows)
            {
                InstitutionReader.Read();
                IdInstitution = InstitutionReader[0].ToString();
            }
            InstitutionReader.Close();

            return IdInstitution;
        }

        public string InsertProjectReport(string idProject, document ProjectDoc)
        {
            string IDPrReport = CheckIfProjectReportExists(idProject, int.Parse( ProjectDoc.idDocument));
            if (IDPrReport == null)
            {
                string sqlInsertProjectReport = "insert into projectreport " + " (Project_idProject, Title, Author, Abstract,LocalId, DocDate, Size, Format, role, scope, privacy )" + " values (@idProject, @Title, @Author, @Abstract, @LocalId, @DocDate, @Size, @Format, @role, @scope, @privacy)";
                MySqlCommand InsertProjectReport = new MySqlCommand(sqlInsertProjectReport, conn);
                InsertProjectReport.Parameters.AddWithValue("@idProject", idProject);
                InsertProjectReport.Parameters.AddWithValue("@Title", ProjectDoc.documentTitle);
                InsertProjectReport.Parameters.AddWithValue("@Abstract", ProjectDoc.documentAbstract);
                InsertProjectReport.Parameters.AddWithValue("@privacy", ProjectDoc.privacy);
                InsertProjectReport.Parameters.AddWithValue("@Author", ProjectDoc.documentAuthor);
                InsertProjectReport.Parameters.AddWithValue("@LocalId", ProjectDoc.idDocument);
                InsertProjectReport.Parameters.AddWithValue("@DocDate", ProjectDoc.documentDate);
                InsertProjectReport.Parameters.AddWithValue("@scope", ProjectDoc.scope);
                InsertProjectReport.Parameters.AddWithValue("@role", ProjectDoc.documentRole);
                InsertProjectReport.Parameters.AddWithValue("@Size", ProjectDoc.documentSize);
                InsertProjectReport.Parameters.AddWithValue("@Format", ProjectDoc.documentFormat);


                //returns long I have to check it
                InsertProjectReport.ExecuteNonQuery();
                IDPrReport = InsertProjectReport.LastInsertedId.ToString();
            }
            return IDPrReport;

        }
        public string CheckIfProjectReportExists(string ProjectID, int ReportLocalID)
        {
            string SqlselectReport = "select idProjectReport from projectreport where localId = @reportLocalID AND Project_idProject = @projectID";
            string ReportID = null;
            MySqlCommand SelectProjectReport = new MySqlCommand(SqlselectReport, conn);
            SelectProjectReport.Parameters.AddWithValue("@ReportLocalID", ReportLocalID);
            SelectProjectReport.Parameters.AddWithValue("@ProjectID", ProjectID);
            MySqlDataReader ProjectReport = SelectProjectReport.ExecuteReader();
            if (ProjectReport.HasRows)
            {
                ProjectReport.Read();
                ReportID = ProjectReport[0].ToString();
            }
            ProjectReport.Close();
            return ReportID;
        }

        public string InsertSpecimen(string PrID, specimen Spec)
        {
            string idSpecimen = CheckIfSpecimenExists(Spec.idSpecimen.ToString(), PrID);
            if (idSpecimen == null)
            {
                string sqlInsertSpec = "insert into Specimen " + " (Project_idProject, name, Width, Length, Height, Depth, SpecimenMass, privacy, LocalID)" + " values (@Projectid, @name, @Width, @Length, @Height, @Depth, @SpecimenMass, @privacy, @LocalID)";
                MySqlCommand insertSpec = new MySqlCommand(sqlInsertSpec, conn);

                insertSpec.Parameters.AddWithValue("@Projectid", PrID);
                insertSpec.Parameters.AddWithValue("@name", Spec.specimenName);
                insertSpec.Parameters.AddWithValue("@Width", Spec.maxWidth);
                insertSpec.Parameters.AddWithValue("@Length", Spec.maxLength);
                insertSpec.Parameters.AddWithValue("@Height", Spec.maxHeight);
                insertSpec.Parameters.AddWithValue("@Depth", Spec.maxDepth);
                insertSpec.Parameters.AddWithValue("@SpecimenMass", Spec.specimenMass);
                insertSpec.Parameters.AddWithValue("@privacy", Spec.privacy);
                insertSpec.Parameters.AddWithValue("@LocalID", Spec.idSpecimen);

                insertSpec.ExecuteNonQuery();
                //returns long I have to check it
                idSpecimen = insertSpec.LastInsertedId.ToString();
            }
            return idSpecimen;
        }
        public string CheckIfSpecimenExists(string SpecimenLocalID, string ProjectID)
        {
            string sqlSelectSpecimen = "select Specimen.idSpecimen from Specimen, Project where Specimen.LocalId = @SpecimenLocalId AND Specimen.Project_idProject = @idproject ";
            string SpecimenId = null;
            MySqlCommand selectSpecimen = new MySqlCommand(sqlSelectSpecimen, conn);
            selectSpecimen.Parameters.AddWithValue("@SpecimenLocalId", SpecimenLocalID);
            selectSpecimen.Parameters.AddWithValue("@idproject", ProjectID);
            MySqlDataReader SpecimenReader = selectSpecimen.ExecuteReader();
            if (SpecimenReader.HasRows)
            {
                SpecimenReader.Read();
                SpecimenId = SpecimenReader[0].ToString();
            }

            SpecimenReader.Close();
            return SpecimenId;
        }


        public string CheckIfScalationExists(scaling scalation, string idspecimen)
        {
            string sqlSelectScalation = "select idSimilitude from Similitude where prototypeModel_ratio = @prototypeModel_ratio AND scaledPropertyName = @scaledPropertyName  AND specimen = @specimen ";
            string ScalationId = null;
            MySqlCommand selectScalation = new MySqlCommand(sqlSelectScalation, conn);
            selectScalation.Parameters.AddWithValue("@specimen", idspecimen);
            selectScalation.Parameters.AddWithValue("@prototypeModel_ratio", scalation.prototypeModelratio);
            selectScalation.Parameters.AddWithValue("@scaledPropertyName", scalation.scaledPropertyName);
            //selectScalation.Parameters.AddWithValue("@LocalId", Scalation.idScalation);

            //LocalId
            MySqlDataReader Similitude = selectScalation.ExecuteReader();
            if (Similitude.HasRows)
            {
                Similitude.Read();
                ScalationId = Similitude[0].ToString();
            }

            Similitude.Close();
            return ScalationId;
        }
        public string InsertSimilitude(scaling Scalation, string Idspecimen)
        {

            string SimilitudeId = CheckIfScalationExists(Scalation, Idspecimen);

            if (SimilitudeId == null)
            {
                string sqlInsertSimilitude = "insert into Similitude" + " (prototypeModel_ratio,scaledPropertyName, specimen)" + " values ( @prototypeModel_ratio, @scaledPropertyName, @specimen)";
                MySqlCommand InsertSimilitude = new MySqlCommand(sqlInsertSimilitude, conn);
                InsertSimilitude.Parameters.AddWithValue("@prototypeModel_ratio", Scalation.prototypeModelratio);
                InsertSimilitude.Parameters.AddWithValue("@scaledPropertyName", Scalation.scaledPropertyName);
                InsertSimilitude.Parameters.AddWithValue("@specimen", Idspecimen);
                //InsertSimilitude.Parameters.AddWithValue("@LocalId", Scalation.idScalation);

                InsertSimilitude.ExecuteNonQuery();
                //returns long I have to check it
                SimilitudeId = InsertSimilitude.LastInsertedId.ToString();
            }
            return SimilitudeId;

        }

        public string InsertSpecimenReport(string idSpecimen, document SpecimenDoc)
        {
            string IdSpecReport = CheckIfSpecReportExists(idSpecimen,int.Parse( SpecimenDoc.idDocument));
            if (IdSpecReport == null)
            {
                string sqlInsertSpecimenReport = "insert into specimenreport " + " (specimen_idspecimen, Title, Author, Abstract,LocalId, DocDate, Size, Format, role, scope, privacy )" + " values (@idSpecimen, @Title, @Author, @Abstract, @LocalId, @DocDate, @Size, @Format, @role, @scope, @privacy)";
                MySqlCommand InsertSpecReport = new MySqlCommand(sqlInsertSpecimenReport, conn);
                InsertSpecReport.Parameters.AddWithValue("@idSpecimen", idSpecimen);
                InsertSpecReport.Parameters.AddWithValue("@Title", SpecimenDoc.documentTitle);
                InsertSpecReport.Parameters.AddWithValue("@Abstract", SpecimenDoc.documentAbstract);
                InsertSpecReport.Parameters.AddWithValue("@privacy", SpecimenDoc.privacy);
                InsertSpecReport.Parameters.AddWithValue("@Author", SpecimenDoc.documentAuthor);
                InsertSpecReport.Parameters.AddWithValue("@LocalId", SpecimenDoc.idDocument);
                InsertSpecReport.Parameters.AddWithValue("@DocDate", SpecimenDoc.documentDate);
                InsertSpecReport.Parameters.AddWithValue("@scope", SpecimenDoc.scope);
                InsertSpecReport.Parameters.AddWithValue("@role", SpecimenDoc.documentRole);
                InsertSpecReport.Parameters.AddWithValue("@Size", SpecimenDoc.documentSize);
                InsertSpecReport.Parameters.AddWithValue("@Format", SpecimenDoc.documentFormat);

                //returns long I have to check it
                InsertSpecReport.ExecuteNonQuery();
                IdSpecReport = InsertSpecReport.LastInsertedId.ToString();
            }
            return IdSpecReport;
        }
        public string CheckIfSpecReportExists(string idSpecimen, int IdLocalSpecimenReport)
        {
            string IdSpecimenReport = null;

            string sqlSelectSpecimenReport = "select idSpecimenReport from specimenreport where Specimen_idSpecimen = @idSpecimen AND LocalId = @LocalId";
            MySqlCommand SelectSpecimenReport = new MySqlCommand(sqlSelectSpecimenReport, conn);
            SelectSpecimenReport.Parameters.AddWithValue("@idSpecimen", idSpecimen);
            SelectSpecimenReport.Parameters.AddWithValue("@LocalId", IdLocalSpecimenReport);

            MySqlDataReader SpecimenReport = SelectSpecimenReport.ExecuteReader();
            if (SpecimenReport.HasRows)
            {
                SpecimenReport.Read();
                IdSpecimenReport = SpecimenReport[0].ToString();
            }
            SpecimenReport.Close();
            return IdSpecimenReport;


        }

        public string CheckIfStructuralElement(string idspecimen, structuralComponent structElement)
        {
            string idstructuralElement = null;

            string sqlStructuralElement = "select StructuralElement.idStructuralElement from StructuralElement where StructuralElement.Specimen_idSpecimen = @idspecimen and Name = @StrElementName and Type = @StrElementType";
            MySqlCommand StructuralElement = new MySqlCommand(sqlStructuralElement, conn);
            StructuralElement.Parameters.AddWithValue("@idspecimen", idspecimen);
            StructuralElement.Parameters.AddWithValue("@StrElementName", structElement.structuralComponentName);
            StructuralElement.Parameters.AddWithValue("@StrElementType", structElement.structuralComponentType);

            MySqlDataReader StructuralElementReader = StructuralElement.ExecuteReader();
            if (StructuralElementReader.HasRows)
            {
                StructuralElementReader.Read();
                idstructuralElement = StructuralElementReader[0].ToString();
            }
            StructuralElementReader.Close();

            return idstructuralElement;
        }
        public string InsertStructuralElement(string idSpecimen, structuralComponent SpecStructuralElem)
        {
            string idStrElement = CheckIfStructuralElement(idSpecimen, SpecStructuralElem);
            if (idStrElement == null)
            {
                string sqlInsertStructuralElement = "insert into StructuralElement " + " (Specimen_idSpecimen, Name, type, materialDescription)" + " values (@idSpecimen, @Name, @Type, @materialDescription)";
                MySqlCommand InsertStructuralElement = new MySqlCommand(sqlInsertStructuralElement, conn);
                InsertStructuralElement.Parameters.AddWithValue("@idSpecimen", idSpecimen);
                InsertStructuralElement.Parameters.AddWithValue("@Name", SpecStructuralElem.structuralComponentName);
                //InsertStructuralElement.Parameters.AddWithValue("@LocalId", SpecStructuralElem.idStructuralElement);
                InsertStructuralElement.Parameters.AddWithValue("@materialDescription", SpecStructuralElem.materialDescription);
                InsertStructuralElement.Parameters.AddWithValue("@type", SpecStructuralElem.structuralComponentType);

                InsertStructuralElement.ExecuteNonQuery();
                idStrElement = InsertStructuralElement.LastInsertedId.ToString();
            }

            return idStrElement;
        }

        public int InsertStrElementHasMaterial(string idSpecimen, string idStructuralElement, string idMaterial)
        {
            string sqlInsertStrElementHasMaterial = "insert into structuralelement_has_material " + " ( StructuralElement_Specimen_idSpecimen, StructuralElement_idStructuralElement, Material_idMaterial)" + " values ( @idSpecimen, @idStructuralElement, @idMaterial)";
            MySqlCommand InsertStrElementHasMaterial = new MySqlCommand(sqlInsertStrElementHasMaterial, conn);
            InsertStrElementHasMaterial.Parameters.AddWithValue("@idSpecimen", idSpecimen);
            InsertStrElementHasMaterial.Parameters.AddWithValue("@idMaterial", idMaterial);
            InsertStrElementHasMaterial.Parameters.AddWithValue("@idStructuralElement", idStructuralElement);

            InsertStrElementHasMaterial.ExecuteNonQuery();
            //returns long I have to check it
            return (int)InsertStrElementHasMaterial.LastInsertedId;


        }
        public string InsertMaterial(material NewMaterial)
        {


            string MaterialID = CheckIfMaterialExist(NewMaterial);
            if (MaterialID == null)
            {
                string SqlInsertMaterial = "insert into Material" + " (Name, LocalId) values " + " (@Name, @LocalId)";
                MySqlCommand InsertMaterial = new MySqlCommand(SqlInsertMaterial, conn);
                InsertMaterial.Parameters.AddWithValue("@Name", NewMaterial.materialName);
                InsertMaterial.Parameters.AddWithValue("@LocalId", NewMaterial.idMaterial);
                InsertMaterial.ExecuteNonQuery();
                MaterialID = InsertMaterial.LastInsertedId.ToString();
            }
            return MaterialID;
        }
        public string CheckIfMaterialExist(material Material)
        {
            string MaterialID = null;

            string sqlSelectMaterial = "select idmaterial from material where localid = @localid  and name = @name";
            MySqlCommand SelectMaterial = new MySqlCommand(sqlSelectMaterial, conn);
            SelectMaterial.Parameters.AddWithValue("@localid", Material.idMaterial);
            SelectMaterial.Parameters.AddWithValue("@name", Material.materialName);


            MySqlDataReader materialReader = SelectMaterial.ExecuteReader();
            if (materialReader.HasRows)
            {
                materialReader.Read();
                MaterialID = materialReader[0].ToString();
            }
            materialReader.Close();
            return MaterialID;
        }

        public string InsertStrElementDoc(string idStrElement, document StrElementDoc)
        {
            string idStrElementDoc = CheckIfStrElementDocExists(idStrElement, StrElementDoc.idDocument.ToString());
            if (idStrElementDoc == null)
            {
                string sqlInsertStrElementDoc = "insert into strelemdoc " + " (structuralElement_idstructuralElement, Title, Author, Abstract,LocalId, DocDate, Size, Format, role, scope, privacy )" + " values (@structuralElement_idstructuralElement, @Title, @Author, @Abstract, @LocalId, @DocDate, @Size, @Format, @role, @scope, @privacy)";
                MySqlCommand InsertStrElementDoc = new MySqlCommand(sqlInsertStrElementDoc, conn);
                InsertStrElementDoc.Parameters.AddWithValue("@structuralElement_idstructuralElement", idStrElement);
                InsertStrElementDoc.Parameters.AddWithValue("@Title", StrElementDoc.documentTitle);
                InsertStrElementDoc.Parameters.AddWithValue("@Abstract", StrElementDoc.documentAbstract);
                InsertStrElementDoc.Parameters.AddWithValue("@privacy", StrElementDoc.privacy);
                InsertStrElementDoc.Parameters.AddWithValue("@Author", StrElementDoc.documentAuthor);
                InsertStrElementDoc.Parameters.AddWithValue("@LocalId", StrElementDoc.idDocument);
                InsertStrElementDoc.Parameters.AddWithValue("@DocDate", StrElementDoc.documentDate);
                InsertStrElementDoc.Parameters.AddWithValue("@scope", StrElementDoc.scope);
                InsertStrElementDoc.Parameters.AddWithValue("@role", StrElementDoc.documentRole);
                InsertStrElementDoc.Parameters.AddWithValue("@Size", StrElementDoc.documentSize);
                InsertStrElementDoc.Parameters.AddWithValue("@Format", StrElementDoc.documentFormat);

                //returns long I have to check it
                InsertStrElementDoc.ExecuteNonQuery();
                idStrElementDoc = InsertStrElementDoc.LastInsertedId.ToString();
            }
            return idStrElementDoc;

        }
        public string CheckIfStrElementDocExists(string idStrElement, string LocalIDDoc)
        {
            string SqlselectStrElementDoc = "select idStrElemDoc from strelemdoc where localId = @reportLocalID AND structuralElement_idstructuralElement = @StrElemID";
            string ReportID = null;
            MySqlCommand SelectStrElementDoc = new MySqlCommand(SqlselectStrElementDoc, conn);
            SelectStrElementDoc.Parameters.AddWithValue("@ReportLocalID", LocalIDDoc);
            SelectStrElementDoc.Parameters.AddWithValue("@StrElemID", idStrElement);
            MySqlDataReader StrElementDoc = SelectStrElementDoc.ExecuteReader();
            if (StrElementDoc.HasRows)
            {
                StrElementDoc.Read();
                ReportID = StrElementDoc[0].ToString();
            }
            StrElementDoc.Close();
            return ReportID;


        }

        public string InsertMaterialDoc(string idMaterial, string idStrElement, document MaterialDoc)
        {

            string idMaterialDoc = CheckIfMaterialDocExists(idStrElement, MaterialDoc.idDocument.ToString(), idMaterial);
            if (idMaterialDoc == null)
            {
                string sqlInsertMaterialDoc = "insert into materialdocument " + " (StructuralElement_idStrElem, Material_IdMaterial,Title, Author, Abstract,LocalId, DocDate, Size, Format, role, scope, privacy )" + " values (@StructuralElement_idStrElem, @Material_IdMaterial, @Title, @Author, @Abstract, @LocalId, @DocDate, @Size, @Format, @role, @scope, @privacy)";
                MySqlCommand InsertMaterialDoc = new MySqlCommand(sqlInsertMaterialDoc, conn);
                InsertMaterialDoc.Parameters.AddWithValue("@StructuralElement_idStrElem", idStrElement);
                InsertMaterialDoc.Parameters.AddWithValue("@Material_IdMaterial", idMaterial);
                InsertMaterialDoc.Parameters.AddWithValue("@Title", MaterialDoc.documentTitle);
                InsertMaterialDoc.Parameters.AddWithValue("@Abstract", MaterialDoc.documentAbstract);
                InsertMaterialDoc.Parameters.AddWithValue("@privacy", MaterialDoc.privacy);
                InsertMaterialDoc.Parameters.AddWithValue("@Author", MaterialDoc.documentAuthor);
                InsertMaterialDoc.Parameters.AddWithValue("@LocalId", MaterialDoc.idDocument);
                InsertMaterialDoc.Parameters.AddWithValue("@DocDate", MaterialDoc.documentDate);
                InsertMaterialDoc.Parameters.AddWithValue("@scope", MaterialDoc.scope);
                InsertMaterialDoc.Parameters.AddWithValue("@role", MaterialDoc.documentRole);
                InsertMaterialDoc.Parameters.AddWithValue("@Size", MaterialDoc.documentSize);
                InsertMaterialDoc.Parameters.AddWithValue("@Format", MaterialDoc.documentFormat);

                //returns long I have to check it
                InsertMaterialDoc.ExecuteNonQuery();
                idMaterialDoc = InsertMaterialDoc.LastInsertedId.ToString();
            }
            return idMaterialDoc;

        }
        public string CheckIfMaterialDocExists(string idStrElement, string LocalIDDoc, string materialID)
        {
            //
            //Material_IdMaterial
            //localID
            //idMaterialDocument

            string SqlselectStrElementDoc = "select idMaterialDocument from materialdocument where localId = @MaterialDocLocalID AND StructuralElement_idStrElem = @StrElemID and Material_IdMaterial = @idmaterial";
            string ReportID = null;
            MySqlCommand SelectStrElementDoc = new MySqlCommand(SqlselectStrElementDoc, conn);
            SelectStrElementDoc.Parameters.AddWithValue("@MaterialDocLocalID", LocalIDDoc);
            SelectStrElementDoc.Parameters.AddWithValue("@StrElemID", idStrElement);
            SelectStrElementDoc.Parameters.AddWithValue("@idmaterial", materialID);
            MySqlDataReader StrElementDoc = SelectStrElementDoc.ExecuteReader();
            if (StrElementDoc.HasRows)
            {
                StrElementDoc.Read();
                ReportID = StrElementDoc[0].ToString();
            }
            StrElementDoc.Close();
            return ReportID;


        }


        public string InsertMaterialNominalProperty(string materialID, nominalProperty NominalProp)
        {
            string IdMaterialNominalProp = CheckIfMaterialNominalProperty(materialID, NominalProp.nominalPropertyName);
            if (IdMaterialNominalProp == null)
            {
                string SqlInsertNominalProperty = "insert into materialnominalproperty" + " (materialID, materialpropertyName,  materialpropertyValue, materialpropertyUnit, observations) values " + " (@materialID, @materialpropertyName,  @materialpropertyValue, @materialpropertyUnit,@observations)";
                MySqlCommand InsertMaterialNominalProperty = new MySqlCommand(SqlInsertNominalProperty, conn);
                InsertMaterialNominalProperty.Parameters.AddWithValue("@materialID", materialID);
                InsertMaterialNominalProperty.Parameters.AddWithValue("@materialpropertyName", NominalProp.nominalPropertyName);
                InsertMaterialNominalProperty.Parameters.AddWithValue("@materialpropertyValue", NominalProp.nominalPropertyValue);
                InsertMaterialNominalProperty.Parameters.AddWithValue("@materialpropertyUnit", NominalProp.nominalPropertyUnit);
                InsertMaterialNominalProperty.Parameters.AddWithValue("@observations", NominalProp.observations);
                InsertMaterialNominalProperty.ExecuteNonQuery();
                IdMaterialNominalProp = InsertMaterialNominalProperty.LastInsertedId.ToString();

            }
            return IdMaterialNominalProp;
        }
        public string CheckIfMaterialNominalProperty(string materialID, string materialpropertyName)
        {
            string MaterialNominalPropertyId = null;
            string sqlSelectMaterialNominalProperty = "select materialnominalproperty.idMaterialNominalProperty from materialnominalproperty where materialnominalproperty.materialID = @materialID AND materialnominalproperty.materialpropertyName = @materialpropertyName";
            MySqlCommand SelectMaterialNominalProperty = new MySqlCommand(sqlSelectMaterialNominalProperty, conn);
            SelectMaterialNominalProperty.Parameters.AddWithValue("@materialID", materialID);
            SelectMaterialNominalProperty.Parameters.AddWithValue("@materialpropertyName", materialpropertyName);
            MySqlDataReader MaterialNominalProperty = SelectMaterialNominalProperty.ExecuteReader();
            if (MaterialNominalProperty.HasRows)
            {
                MaterialNominalProperty.Read();
                MaterialNominalPropertyId = MaterialNominalProperty[0].ToString();
            }
            MaterialNominalProperty.Close();
            return MaterialNominalPropertyId;

        }

        public string InsertMaterialActualProperties(string IdStructuraElement, string idMaterial, actualMeanProperty MaterialActualProperty)
        {
            string IdMaterialActualProp = CheckIfActualMeanPropertyExist(MaterialActualProperty, idMaterial, IdStructuraElement);
            if (IdMaterialActualProp == null)
            {
                string SqlMaterialActProp = "insert into MaterialActualProp" + " (StructuraElement_idStructuralElement,Material_idMaterial,properties,value,unit ,samples, Observations) values " + " (@IdStructuraElement,@idMaterial,@properties,@value,@unit,@samples, @Observations)";
                MySqlCommand MaterialActProp = new MySqlCommand(SqlMaterialActProp, conn);
                MaterialActProp.Parameters.AddWithValue("@IdStructuraElement", IdStructuraElement);
                MaterialActProp.Parameters.AddWithValue("@idMaterial", idMaterial);
                MaterialActProp.Parameters.AddWithValue("@properties", MaterialActualProperty.actualMeanPropertyName);
                MaterialActProp.Parameters.AddWithValue("@value", MaterialActualProperty.actualMeanPropertyValue);
                MaterialActProp.Parameters.AddWithValue("@unit", MaterialActualProperty.actualMeanPropertyUnit);
                MaterialActProp.Parameters.AddWithValue("@samples", MaterialActualProperty.numberOfSamples);
                //MaterialActProp.Parameters.AddWithValue("@LocalId", MaterialActualProperty.idActualMeanProperty);
                MaterialActProp.Parameters.AddWithValue("@Observations", MaterialActualProperty.observations);
                MaterialActProp.ExecuteNonQuery();
                IdMaterialActualProp = MaterialActProp.LastInsertedId.ToString();
            }
            return IdMaterialActualProp;
        }
        public string CheckIfActualMeanPropertyExist(actualMeanProperty MaterialActualProp, string idMaterial, string StructuralElementID)
        {
            string ActualMeanPropertyId = null;

            //string[] keys = null;
            string sqlSelectActualMeanProperty = "select idMaterialActualProp from materialactualprop where  materialactualprop.properties = @name AND materialactualprop.Material_idMaterial = @idmaterial AND materialactualprop.StructuraElement_idStructuralElement = @idStructuralElement";
            MySqlCommand SelectActualMeanProperty = new MySqlCommand(sqlSelectActualMeanProperty, conn);
            SelectActualMeanProperty.Parameters.AddWithValue("@name", MaterialActualProp.actualMeanPropertyName);
            SelectActualMeanProperty.Parameters.AddWithValue("@idStructuralElement", StructuralElementID);
            SelectActualMeanProperty.Parameters.AddWithValue("@idmaterial", idMaterial);
            MySqlDataReader ActualMeanProperty = SelectActualMeanProperty.ExecuteReader();
            if (ActualMeanProperty.HasRows)
            {
                ActualMeanProperty.Read();
                ActualMeanPropertyId = ActualMeanProperty[0].ToString();
            }
            ActualMeanProperty.Close();
            return ActualMeanPropertyId;
        }

        public string InsertCompExp(string specimenId, experiment Exp)
        {
            string idCompExp = CheckIdCompExpExists(Exp.idExpComp.ToString(), specimenId);
            if (idCompExp == null)
            {

                string sqlInsertCompExp = "insert into CompExp " + " (Specimen_IDSpecimen, Name, CreationDate, Repetition, PeakExcitUnit,PeakExcitValue, LocalId, Type, ExpCompType , privacy)" + " values (@Specimen_IDSpecimen, @Name, @CreationDate, @Repetition, @PeakExcitUnit, @PeakExcitValue, @LocalId, @Type, @ExpCompType, @privacy)";
                MySqlCommand insertCompExp = new MySqlCommand(sqlInsertCompExp, conn);
                insertCompExp.Parameters.AddWithValue("@Specimen_IDSpecimen", specimenId);
                insertCompExp.Parameters.AddWithValue("@Name", Exp.expCompName);
                insertCompExp.Parameters.AddWithValue("CreationDate", Exp.experimentComputationDate);
                insertCompExp.Parameters.AddWithValue("@Repetition", Exp.numberOfRepetitions);
                insertCompExp.Parameters.AddWithValue("@PeakExcitUnit", Exp.peakExcitationUnit);
                insertCompExp.Parameters.AddWithValue("@PeakExcitValue", Exp.peakExcitationValue);
                insertCompExp.Parameters.AddWithValue("@LocalId", Exp.idExpComp);
                //insertCompExp.Parameters.AddWithValue("@Type", Exp.expCompType);
                insertCompExp.Parameters.AddWithValue("@Type", "Experiment");
                //insertCompExp.Parameters.AddWithValue("@LoadingCoef", Exp.loadingCoefficient);
                insertCompExp.Parameters.AddWithValue("@ExpCompType", Exp.expCompType);
                insertCompExp.Parameters.AddWithValue("@privacy", Exp.privacy);

                insertCompExp.ExecuteNonQuery();
                idCompExp = insertCompExp.LastInsertedId.ToString();
            }

            return idCompExp;



        }
        public string InsertCompExp(string specimenId, computation Computation)
        {
            string idCompExp = CheckIdCompExpExists(Computation.idExpComp.ToString(), specimenId);
            if (idCompExp == null)
            {
                string sqlInsertCompExp = "insert into CompExp " + " (Specimen_IDSpecimen, Name, CreationDate, Repetition, PeakExcitUnit,PeakExcitValue, LocalId, Type , privacy,ExpCompType)" + " values (@Specimen_IDSpecimen, @Name, @CreationDate, @Repetition, @PeakExcitValue, @PeakExcitUnit, @LocalId, @Type, @privacy,@ExpCompType)";
                MySqlCommand insertCompExp = new MySqlCommand(sqlInsertCompExp, conn);
                insertCompExp.Parameters.AddWithValue("@Specimen_IDSpecimen", specimenId);
                insertCompExp.Parameters.AddWithValue("@Name", Computation.expCompName);
                insertCompExp.Parameters.AddWithValue("CreationDate", Computation.experimentComputationDate);
                insertCompExp.Parameters.AddWithValue("@Repetition", Computation.numberOfRepetitions);
                insertCompExp.Parameters.AddWithValue("@PeakExcitUnit", Computation.peakExcitationUnit);
                insertCompExp.Parameters.AddWithValue("@PeakExcitValue", Computation.peakExcitationValue);
                insertCompExp.Parameters.AddWithValue("@LocalId", Computation.idExpComp);
                insertCompExp.Parameters.AddWithValue("@ExpCompType", Computation.expCompType);
                insertCompExp.Parameters.AddWithValue("@Type", "Computation");
                //insertCompExp.Parameters.AddWithValue("@LoadingCoef", Computation.loadingCoefficient);
                //insertCompExp.Parameters.AddWithValue("@CompExpPerformed", Exp.);
                insertCompExp.Parameters.AddWithValue("@privacy", Computation.privacy);


                insertCompExp.ExecuteNonQuery();
                idCompExp = insertCompExp.LastInsertedId.ToString();
            }

            return idCompExp;



        }
        public string CheckIdCompExpExists(string CompExpLocalId, string SpecimenId)
        {
            string sqlSelectCompExp = "select idCompExp from CompExp where Localid = @CompExpLocalId AND Specimen_idSpecimen =@SpecimenId ";
            string CompExpId = null;
            MySqlCommand selectCompExp = new MySqlCommand(sqlSelectCompExp, conn);
            selectCompExp.Parameters.AddWithValue("@CompExpLocalId", CompExpLocalId);
            selectCompExp.Parameters.AddWithValue("@SpecimenId", SpecimenId);
            MySqlDataReader CompExp = selectCompExp.ExecuteReader();
            if (CompExp.HasRows)
            {
                CompExp.Read();
                CompExpId = CompExp[0].ToString();
            }

            CompExp.Close();
            return CompExpId;

        }

        public int InsertCompExpHasPersonnel(string CompExpId, string PersonnelId)
        {
            string SqlInsertCompExpHasPersonnel = "insert into Comp_has_Personnel" + " (Personnel_idPersonnel,CompExp_idCompExp) values " + " (@idPersonnel,@idCompExp)";
            MySqlCommand InsertCompExpHasPersonnel = new MySqlCommand(SqlInsertCompExpHasPersonnel, conn);
            InsertCompExpHasPersonnel.Parameters.AddWithValue("@idCompExp", CompExpId);
            InsertCompExpHasPersonnel.Parameters.AddWithValue("@idPersonnel", PersonnelId);
            InsertCompExpHasPersonnel.ExecuteNonQuery();
            return (int)InsertCompExpHasPersonnel.LastInsertedId;


        }
        public string CheckIfCompHasPersonnelExist(string CompExpID, string PersonnelID)
        {
            string IdCompExpHasPersonnel = null;
            string SqlSelectCompExpHasPersonnel = "select CompExp_idCompExp from Comp_has_Personnel where Personnel_idPersonnel = @idPersonnel AND CompExp_idCompExp = @idCompExp";
            MySqlCommand SelectCompExpHasPersonnel = new MySqlCommand(SqlSelectCompExpHasPersonnel, conn);
            SelectCompExpHasPersonnel.Parameters.AddWithValue("@idCompExp", CompExpID);
            SelectCompExpHasPersonnel.Parameters.AddWithValue("@idPersonnel", PersonnelID);
            SelectCompExpHasPersonnel.ExecuteNonQuery();
            MySqlDataReader CompExpHasPersonnelReader = SelectCompExpHasPersonnel.ExecuteReader();
            if (CompExpHasPersonnelReader.HasRows)
            {
                CompExpHasPersonnelReader.Read();
                IdCompExpHasPersonnel = CompExpHasPersonnelReader[0].ToString();
            }
            CompExpHasPersonnelReader.Close();

            return IdCompExpHasPersonnel;

        }

        public string InsertCompExpPictures(string idCompExp, image CompExpPic)
        {
            string IdExpImg = CheckIfExpCompPicExist(idCompExp, CompExpPic);
            if (IdExpImg == null)
            {
                string sqlInsert = "Insert Into CompExpPictures" + " (CompExp_idCompExp, Title, CreationDate, LocalId, Author,Format,Size,privacy,summary,role) Values" + " (@idCompExp,@Title, @CreationDate, @LocalId, @Author, @Format, @Size, @privacy, @summary, @role) ";
                MySqlCommand insert = new MySqlCommand(sqlInsert, conn);
                insert.Parameters.AddWithValue("@idCompExp", idCompExp);
                insert.Parameters.AddWithValue("@Title", CompExpPic.imageName);
                insert.Parameters.AddWithValue("@CreationDate", CompExpPic.imageDate);
                insert.Parameters.AddWithValue("@LocalId", CompExpPic.idImage);
                insert.Parameters.AddWithValue("@Author", CompExpPic.imageAuthor);
                insert.Parameters.AddWithValue("@Format", CompExpPic.imageFormat);
                insert.Parameters.AddWithValue("@Size", CompExpPic.imageSize);
                insert.Parameters.AddWithValue("@privacy", CompExpPic.privacy);
                insert.Parameters.AddWithValue("@summary", CompExpPic.summary);
                insert.Parameters.AddWithValue("@Role", CompExpPic.documentRole);

                insert.ExecuteNonQuery();
                IdExpImg = insert.LastInsertedId.ToString();
            }
            return IdExpImg;
        }
        public string CheckIfExpCompPicExist(string idCompExp, image CompExpPic)
        {

            string picId = null;
            string sqlSelectPic = "select idCompExpPictures from CompExpPictures where CompExp_idCompExp = @idCompExp  AND LocalId = @LocalId";
            MySqlCommand SelectPic = new MySqlCommand(sqlSelectPic, conn);
            SelectPic.Parameters.AddWithValue("@idCompExp", idCompExp);
            SelectPic.Parameters.AddWithValue("@LocalId", CompExpPic.idImage);

            MySqlDataReader pic = SelectPic.ExecuteReader();
            if (pic.HasRows)
            {
                pic.Read();
                picId = pic[0].ToString();
            }
            pic.Close();
            return picId;

        }

        public string InsertVideo(string idCompExp, video NewVideo)
        {
            string IdVideo = CheckIfVideoExist(NewVideo, idCompExp);
            if (IdVideo == null)
            {
                string sqlInsertVideo = "insert into Video" + " (CompExp_idCompExp, Name, LocalId, CreationDate, Format, Role, Size, summary, privacy)" + " values ( @idCompExp, @Name, @LocalId, @CreationDate, @Format, @Role, @Size, @summary, @privacy)";
                MySqlCommand InsertVideo = new MySqlCommand(sqlInsertVideo, conn);
                InsertVideo.Parameters.AddWithValue("@idCompExp", idCompExp);
                InsertVideo.Parameters.AddWithValue("@Name", NewVideo.videoName);
                InsertVideo.Parameters.AddWithValue("@LocalId", NewVideo.idVideo);
                InsertVideo.Parameters.AddWithValue("@CreationDate", NewVideo.videoDate);
                InsertVideo.Parameters.AddWithValue("@Format", NewVideo.videoFormat);
                InsertVideo.Parameters.AddWithValue("@Role", NewVideo.videoRole);
                InsertVideo.Parameters.AddWithValue("@Size", NewVideo.videoSize);
                InsertVideo.Parameters.AddWithValue("@summary", NewVideo.summary);
                InsertVideo.Parameters.AddWithValue("@privacy", NewVideo.privacy);
                InsertVideo.ExecuteNonQuery();
                IdVideo = InsertVideo.LastInsertedId.ToString();

            }
            //returns long I have to check it
            return IdVideo;
        }
        public string CheckIfVideoExist(video VideoCompExp, string CompExpId)
        {
            string VideoID = null;

            string sqlSelectVideo = "select idVideo from video where CompExp_idCompExp = @idCompExp AND LocalId = @LocalId";
            MySqlCommand SelectVideo = new MySqlCommand(sqlSelectVideo, conn);
            SelectVideo.Parameters.AddWithValue("@idCompExp", CompExpId);
            SelectVideo.Parameters.AddWithValue("@LocalId", VideoCompExp.idVideo);


            MySqlDataReader VideoReader = SelectVideo.ExecuteReader();
            if (VideoReader.HasRows)
            {
                VideoReader.Read();
                VideoID = VideoReader[0].ToString();
            }
            VideoReader.Close();
            return VideoID;

        }

        public string InsertDocument(document Doc)
        {
            string IDDocument = CheckIfDocumentExists(Doc);
            if (IDDocument == null)
            {
                string sqlInsertDocument = "insert into Document " + " (Title, Author, Abstract, LocalId, CreationDate, Size, Format, role, scope, privacy )" + " values (@Title, @Author, @Abstract, @LocalId, @CreationDate, @Size, @Format, @role, @scope, @privacy)";
                MySqlCommand InsertDocument = new MySqlCommand(sqlInsertDocument, conn);

                InsertDocument.Parameters.AddWithValue("@Title", Doc.documentTitle);
                InsertDocument.Parameters.AddWithValue("@Abstract", Doc.documentAbstract);
                InsertDocument.Parameters.AddWithValue("@privacy", Doc.privacy);
                InsertDocument.Parameters.AddWithValue("@Author", Doc.documentAuthor);
                InsertDocument.Parameters.AddWithValue("@LocalId", Doc.idDocument);
                InsertDocument.Parameters.AddWithValue("@CreationDate", Doc.documentDate);
                InsertDocument.Parameters.AddWithValue("@scope", Doc.scope);
                InsertDocument.Parameters.AddWithValue("@role", Doc.documentRole);
                InsertDocument.Parameters.AddWithValue("@Size", Doc.documentSize);
                InsertDocument.Parameters.AddWithValue("@Format", Doc.documentFormat);

                //returns long I have to check it
                InsertDocument.ExecuteNonQuery();
                IDDocument = InsertDocument.LastInsertedId.ToString();
            }
            return IDDocument;

        }
        public string CheckIfDocumentExists(document Doc)
        {
            string SqlselectDoc = "select iddocument from document where localId = @LocalID AND title=@title and abstract =@abstract and privacy = @privacy and author = @author and creationdate =@creationdate and scope =@scope and role = @role and format= @format and size =@size";
            string DocID = null;
            MySqlCommand SelectDoc = new MySqlCommand(SqlselectDoc, conn);

            SelectDoc.Parameters.AddWithValue("@Title", Doc.documentTitle);
            SelectDoc.Parameters.AddWithValue("@Abstract", Doc.documentAbstract);
            SelectDoc.Parameters.AddWithValue("@privacy", Doc.privacy);
            SelectDoc.Parameters.AddWithValue("@Author", Doc.documentAuthor);
            SelectDoc.Parameters.AddWithValue("@LocalId", Doc.idDocument);
            SelectDoc.Parameters.AddWithValue("@CreationDate", Doc.documentDate);
            SelectDoc.Parameters.AddWithValue("@scope", Doc.scope);
            SelectDoc.Parameters.AddWithValue("@role", Doc.documentRole);
            SelectDoc.Parameters.AddWithValue("@Size", Doc.documentSize);
            SelectDoc.Parameters.AddWithValue("@Format", Doc.documentFormat);

            MySqlDataReader DocReader = SelectDoc.ExecuteReader();
            if (DocReader.HasRows)
            {
                DocReader.Read();
                DocID = DocReader[0].ToString();
            }
            DocReader.Close();
            return DocID;
        }


        public string InsertCompExpHasDoc(string idCompExp, string iddocument)
        {
            string IDCompExpHasDoc = CheckIfCompExpHasDocExists(idCompExp, iddocument);
            if (IDCompExpHasDoc == null)
            {
                string sqlInsertCompExpHasDoc = "insert into compexpdocs " + " (CompExp_idCompExp, Document_iddocument)" + " values (@idCompExp, @iddocument)";
                MySqlCommand InsertCompExpHasDoc = new MySqlCommand(sqlInsertCompExpHasDoc, conn);

                InsertCompExpHasDoc.Parameters.AddWithValue("@idCompExp", idCompExp);
                InsertCompExpHasDoc.Parameters.AddWithValue("@iddocument", iddocument);

                //returns long I have to check it
                InsertCompExpHasDoc.ExecuteNonQuery();
                IDCompExpHasDoc = InsertCompExpHasDoc.LastInsertedId.ToString();
            }
            return IDCompExpHasDoc;

        }
        public string CheckIfCompExpHasDocExists(string idCompExp, string iddocument)
        {
            string SqlselectCompExpHasDoc = "select idCompExpDocs from compexpdocs where CompExp_idCompExp = @idCompExp AND Document_iddocument=@iddocument";
            string CompExpHasDocID = null;
            MySqlCommand SelectCompExpHasDoc = new MySqlCommand(SqlselectCompExpHasDoc, conn);

            SelectCompExpHasDoc.Parameters.AddWithValue("@idCompExp", idCompExp);
            SelectCompExpHasDoc.Parameters.AddWithValue("@iddocument", iddocument);


            MySqlDataReader CompExpHasDocReader = SelectCompExpHasDoc.ExecuteReader();
            if (CompExpHasDocReader.HasRows)
            {
                CompExpHasDocReader.Read();
                CompExpHasDocID = CompExpHasDocReader[0].ToString();
            }
            CompExpHasDocReader.Close();
            return CompExpHasDocID;
        }

        public string InsertDLC(detLoadChar DLC)
        {

            string IdNominalLoading = CheckIfDLCExist(DLC);
            if (IdNominalLoading == null)
            {
                string SqlNominalLoading = "insert into DLC" + " (additionalparameter, loadingCoef, Name, Notes ) values " + " ( @additionalparameter, @loadingCoef, @Name, @Notes )";
                MySqlCommand InsertNominalLoading = new MySqlCommand(SqlNominalLoading, conn);

                InsertNominalLoading.Parameters.AddWithValue("@additionalParameter", DLC.additionalParameter);
                InsertNominalLoading.Parameters.AddWithValue("@loadingCoef", DLC.loadingCoefficient);
                InsertNominalLoading.Parameters.AddWithValue("@Name", DLC.nominalLoadingName);
                InsertNominalLoading.Parameters.AddWithValue("@Notes", DLC.notes);
                InsertNominalLoading.ExecuteNonQuery();

                IdNominalLoading = InsertNominalLoading.LastInsertedId.ToString();
            }

            return IdNominalLoading;
        }
        public string CheckIfDLCExist(detLoadChar DLC)
        {
            string IdDLC = null;

            string sqlDLC = "select iddlc from DLC where additionalparameter = @additionalParameter and loadingCoef = @loadingCoef and Name = @Name and Notes = @notes";
            MySqlCommand sqlDLCCmd = new MySqlCommand(sqlDLC, conn);
            //NominalLoadingCmd.Parameters.AddWithValue("@LocalId", LocalId);
            sqlDLCCmd.Parameters.AddWithValue("@additionalParameter", DLC.additionalParameter);
            sqlDLCCmd.Parameters.AddWithValue("@loadingCoef", DLC.loadingCoefficient);
            sqlDLCCmd.Parameters.AddWithValue("@Name", DLC.nominalLoadingName);
            sqlDLCCmd.Parameters.AddWithValue("@Notes", DLC.notes);

            MySqlDataReader DLCReader = sqlDLCCmd.ExecuteReader();
            if (DLCReader.HasRows)
            {
                DLCReader.Read();
                IdDLC = DLCReader[0].ToString();
            }
            DLCReader.Close();

            return IdDLC;
        }

        public string InsertCompExp_has_dlc(string iddlc, string CompExpId)
        {

            string IdNominalLoading = CheckIfcompexp_has_dlcExists(iddlc, CompExpId);
            if (IdNominalLoading == null)
            {
                string SqlNominalLoading = "insert into compexp_has_dlc" + " (dlc_iddlc, compexp_idcompexp ) values " + " ( @iddlc, @idcompexp)";
                MySqlCommand InsertNominalLoading = new MySqlCommand(SqlNominalLoading, conn);

                InsertNominalLoading.Parameters.AddWithValue("@iddlc", iddlc);
                InsertNominalLoading.Parameters.AddWithValue("@idcompexp", CompExpId);
                InsertNominalLoading.ExecuteNonQuery();

                IdNominalLoading = InsertNominalLoading.LastInsertedId.ToString();
            }

            return IdNominalLoading;

        }
        public string CheckIfcompexp_has_dlcExists(string iddlc, string CompExpId)
        {
            string Idcompexp_has_dlc = null;
            string SqlCheckifExists = "select idcompexp_has_dlc from compexp_has_dlc where dlc_iddlc =@iddlc and compexp_idcompexp = @idcompexp";
            MySqlCommand Selectcompexp_has_dlc = new MySqlCommand(SqlCheckifExists, conn);
            Selectcompexp_has_dlc.Parameters.AddWithValue("@iddlc", iddlc);
            Selectcompexp_has_dlc.Parameters.AddWithValue("@idcompexp", CompExpId);
            Selectcompexp_has_dlc.ExecuteNonQuery();

            MySqlDataReader DlcReader = Selectcompexp_has_dlc.ExecuteReader();
            if (DlcReader.HasRows)
            {
                DlcReader.Read();
                Idcompexp_has_dlc = DlcReader[0].ToString();
            }
            DlcReader.Close();
            return Idcompexp_has_dlc;
        }


        public string InsertOriginalLoading(originalLoadingSignal loadingSignal,string idSignal)
        {
           
            

            string IdOriginalLoading = CheckIfOriginalLoadingSignalExist(loadingSignal, idSignal);


            if (IdOriginalLoading == null)
            {
                string SqlInsertOriginalLoading = "insert into originalloading " + " (Name, Nature, Source, peakExcitationUnit,peakExcitationValue,Signal_idSignal) values " + " (@Name, @Nature, @Source, @peakExcitationUnit,@peakExcitationValue,@Signal_idSignal)";
                MySqlCommand InsertOriginalLoading = new MySqlCommand(SqlInsertOriginalLoading, conn);
                InsertOriginalLoading.Parameters.AddWithValue("@Name", loadingSignal.originalLoadingName);
                InsertOriginalLoading.Parameters.AddWithValue("@peakExcitationUnit", loadingSignal.peakExcitationUnit);
                InsertOriginalLoading.Parameters.AddWithValue("@peakExcitationValue", loadingSignal.peakExcitationValue);
                InsertOriginalLoading.Parameters.AddWithValue("@source", loadingSignal.source);
                InsertOriginalLoading.Parameters.AddWithValue("@nature", loadingSignal.nature);
                InsertOriginalLoading.Parameters.AddWithValue("@Signal_idSignal", idSignal);



                InsertOriginalLoading.ExecuteNonQuery();
                IdOriginalLoading = InsertOriginalLoading.LastInsertedId.ToString();
            }
            return IdOriginalLoading;

        }
        public string CheckIfOriginalLoadingSignalExist(originalLoadingSignal OriginalSignal, string idsignal)
        {
            string IdOriginalSignal = null;

            string sqlSelectOriginalSignal = "select idoriginalloading from originalloading where Name = @Name AND Nature = @Nature AND Source = @Source AND peakExcitationValue = @peakExcitationValue  AND peakExcitationUnit = @peakExcitationUnit AND Signal_idSignal = @idsignal";
            MySqlCommand SelectOriginalSignal = new MySqlCommand(sqlSelectOriginalSignal, conn);
            SelectOriginalSignal.Parameters.AddWithValue("@Name", OriginalSignal.originalLoadingName);
            SelectOriginalSignal.Parameters.AddWithValue("@Nature", OriginalSignal.nature);
            SelectOriginalSignal.Parameters.AddWithValue("@Source", OriginalSignal.source);
            SelectOriginalSignal.Parameters.AddWithValue("@peakExcitationValue", OriginalSignal.peakExcitationValue);
            SelectOriginalSignal.Parameters.AddWithValue("@peakExcitationUnit", OriginalSignal.peakExcitationUnit);
            SelectOriginalSignal.Parameters.AddWithValue("@idsignal", idsignal);

            //SelectOriginalSignal.Parameters.AddWithValue("@LocalId", OriginalSignal.idOriginalLoadingSignal);

            MySqlDataReader OriginalSignalReader = SelectOriginalSignal.ExecuteReader();
            if (OriginalSignalReader.HasRows)
            {
                OriginalSignalReader.Read();
                IdOriginalSignal = OriginalSignalReader[0].ToString();
            }
            OriginalSignalReader.Close();
            return IdOriginalSignal;

        }

        public string InsertInputSignal(signal InputSignal)
        {
            string idInputSignal = CheckIfInputSignalExists(InputSignal);

            if (idInputSignal == null)
            {
                string SqlInsertSignal = "insert into InputSignal" + " (Label,Attribute,PhysicalQ,Type,Unit,Location,LocalID) values " + " (@SignalLabel,@Attribute,@PhysicalIQ,@type,@Unit,@Location,@LocalID)";
                MySqlCommand insertSignal = new MySqlCommand(SqlInsertSignal, conn);
                insertSignal.Parameters.AddWithValue("@SignalLabel", InputSignal.signalLabel);
                insertSignal.Parameters.AddWithValue("@Attribute", InputSignal.attribute);
                insertSignal.Parameters.AddWithValue("@PhysicalIQ", InputSignal.physicalQuantity);
                insertSignal.Parameters.AddWithValue("@Type", InputSignal.type);
                insertSignal.Parameters.AddWithValue("@Unit", InputSignal.unit);
                insertSignal.Parameters.AddWithValue("@Location", InputSignal.location);
                insertSignal.Parameters.AddWithValue("@privacy", InputSignal.privacy);
                insertSignal.Parameters.AddWithValue("@localID", InputSignal.idSignal);
                insertSignal.Parameters.AddWithValue("@repetition", InputSignal.repetitionNumber);
                insertSignal.ExecuteNonQuery();
                idInputSignal = insertSignal.LastInsertedId.ToString();

            }
            return idInputSignal;

        }
        public string CheckIfInputSignalExists(signal InputSignal)
        {
            string sqlSelectSignal = "select idInputSignal from Inputsignal where Localid = @SignalLocalId AND Label = @Label AND Attribute = @Attribute AND PhysicalQ = @PhysicalQ AND Type  = @Type AND Unit = @Unit and Location = @Location";
            string SignalId = null;
            MySqlCommand selectSignal = new MySqlCommand(sqlSelectSignal, conn);
            selectSignal.Parameters.AddWithValue("@SignalLocalId", InputSignal.idSignal);
            selectSignal.Parameters.AddWithValue("@Label", InputSignal.signalLabel);
            selectSignal.Parameters.AddWithValue("@Attribute", InputSignal.attribute);
            selectSignal.Parameters.AddWithValue("@PhysicalQ", InputSignal.physicalQuantity);
            selectSignal.Parameters.AddWithValue("@Type", InputSignal.type);
            selectSignal.Parameters.AddWithValue("@Unit", InputSignal.unit);
            selectSignal.Parameters.AddWithValue("@Location", InputSignal.location);
            selectSignal.Parameters.AddWithValue("@privacy", InputSignal.privacy);
            selectSignal.Parameters.AddWithValue("@localID", InputSignal.idSignal);
            selectSignal.Parameters.AddWithValue("@repetition", InputSignal.repetitionNumber);
            MySqlDataReader Signal = selectSignal.ExecuteReader();
            if (Signal.HasRows)
            {
                Signal.Read();
                SignalId = Signal[0].ToString();
            }
            Signal.Close();
            return SignalId;
        }

        public string InsertOutputSignal(string ExpCompid, signal OutputSignal)
        {
            string IdSignalResult = CheckIfOutputSignalExists(OutputSignal.idSignal.ToString(), ExpCompid);
            if (IdSignalResult == null)
            {
                string SqlInsertSignal = "insert into SignalResult" + " (CompExp_idCompExp, SignaLabel, Attribute, PhysicalQ, Type, Unit, Location, LocalID, repetition, privacy) values " + " (@CompExpid, @SignalLabel, @Attribute, @PhysicalIQ, @type, @Unit, @Location, @LocalID, @repetition, @privacy)";
                MySqlCommand insertSignal = new MySqlCommand(SqlInsertSignal, conn);
                insertSignal.Parameters.AddWithValue("@CompExpid", ExpCompid);
                insertSignal.Parameters.AddWithValue("@SignalLabel", OutputSignal.signalLabel);
                insertSignal.Parameters.AddWithValue("@Attribute", OutputSignal.attribute);
                insertSignal.Parameters.AddWithValue("@PhysicalIQ", OutputSignal.physicalQuantity);
                insertSignal.Parameters.AddWithValue("@Type", OutputSignal.type);
                insertSignal.Parameters.AddWithValue("@Unit", OutputSignal.unit);
                insertSignal.Parameters.AddWithValue("@Location", OutputSignal.location);
                insertSignal.Parameters.AddWithValue("@localID", OutputSignal.idSignal);
                insertSignal.Parameters.AddWithValue("@repetition", OutputSignal.repetitionNumber);
                insertSignal.Parameters.AddWithValue("@privacy", OutputSignal.privacy);

                insertSignal.ExecuteNonQuery();
                IdSignalResult = insertSignal.LastInsertedId.ToString();
            }
            return IdSignalResult;

        }
        public string CheckIfOutputSignalExists(string SignalLocalId, string CompExpId)
        {
            string sqlSelectSignal = "select idSignalResult from signalresult where Localid = @SignalLocalId AND CompExp_idCompExp =@CompExpId ";
            string SignalId = null;
            MySqlCommand selectSignal = new MySqlCommand(sqlSelectSignal, conn);
            selectSignal.Parameters.AddWithValue("@SignalLocalId", SignalLocalId);
            selectSignal.Parameters.AddWithValue("@CompExpId", CompExpId);
            MySqlDataReader Signal = selectSignal.ExecuteReader();
            if (Signal.HasRows)
            {
                Signal.Read();
                SignalId = Signal[0].ToString();
            }
            Signal.Close();
            return SignalId;
        }

        public string InsertMeshModel(meshModel NewMeshModel, string idCompExp)
        {
            string MeshModelID = CheckIfMeshModelExists(NewMeshModel, idCompExp);
            if (MeshModelID == null)
            {
                string SqlInserMeshModel = "insert into MeshModel" + " (Nonlinearity,SymmetryType,CompExp_idcompexp) values " + " (@Nonlinearity,@SymmetryType,@idcompexp)";
                MySqlCommand InserMeshModel = new MySqlCommand(SqlInserMeshModel, conn);
                InserMeshModel.Parameters.AddWithValue("@Nonlinearity", NewMeshModel.materialNonlinearity);
                InserMeshModel.Parameters.AddWithValue("@SymmetryType", NewMeshModel.materialSymmetryType);
                // InserMeshModel.Parameters.AddWithValue("@LocalId", NewMeshModel.idMeshModel);
                InserMeshModel.Parameters.AddWithValue("@idCompExp", idCompExp);
                InserMeshModel.ExecuteNonQuery();
                MeshModelID = InserMeshModel.LastInsertedId.ToString();
            }
            return MeshModelID;
        }
        public string CheckIfMeshModelExists(meshModel NewMeshModel, string idCompExp)
        {
            string SqlselectMeshModel = "select idMeshModel from meshmodel where Nonlinearity = @Nonlinearity AND SymmetryType = @SymmetryType and CompExp_idcompexp = @idcompexp";
            string MeshModelID = null;
            MySqlCommand SelectMeshModel = new MySqlCommand(SqlselectMeshModel, conn);
            SelectMeshModel.Parameters.AddWithValue("@Nonlinearity", NewMeshModel.materialNonlinearity);
            SelectMeshModel.Parameters.AddWithValue("@SymmetryType", NewMeshModel.materialSymmetryType);
            SelectMeshModel.Parameters.AddWithValue("@idcompexp", idCompExp);

            MySqlDataReader MeshModelReader = SelectMeshModel.ExecuteReader();
            if (MeshModelReader.HasRows)
            {
                MeshModelReader.Read();
                MeshModelID = MeshModelReader[0].ToString();
            }
            MeshModelReader.Close();
            return MeshModelID;
        }

        public string InsertMeshModelDoc(document MeshModelDoc, string MeshModelId)
        {
            string IDMeshModelDoc = CheckIfMeshModelDocExists(MeshModelDoc.idDocument.ToString(), MeshModelId);
            if (IDMeshModelDoc == null)
            {
                string SqlInserMeshModelDoc = "insert into meshmodeldocuments" + " (LocalId,Title,Author, Size, Format, MeshModel_IdMeshModel, Abstract, privacy, CreationDate, scope, role) values " + " (@LocalId, @Title, @Author, @Size, @Format, @IdMeshModel, @Abstract, @privacy, @CreationDate, @scope, @role)";
                MySqlCommand InserMeshModelDoc = new MySqlCommand(SqlInserMeshModelDoc, conn);
                InserMeshModelDoc.Parameters.AddWithValue("@LocalId", MeshModelDoc.idDocument);
                InserMeshModelDoc.Parameters.AddWithValue("@Title", MeshModelDoc.documentTitle);
                InserMeshModelDoc.Parameters.AddWithValue("@Author", MeshModelDoc.documentAuthor);
                InserMeshModelDoc.Parameters.AddWithValue("@Size", MeshModelDoc.documentSize);
                InserMeshModelDoc.Parameters.AddWithValue("@Format", MeshModelDoc.documentFormat);
                InserMeshModelDoc.Parameters.AddWithValue("@Abstract", MeshModelDoc.documentAbstract);
                InserMeshModelDoc.Parameters.AddWithValue("@IdMeshModel", MeshModelId);
                InserMeshModelDoc.Parameters.AddWithValue("@privacy", MeshModelDoc.privacy);
                InserMeshModelDoc.Parameters.AddWithValue("@CreationDate", MeshModelDoc.documentDate);
                InserMeshModelDoc.Parameters.AddWithValue("@scope", MeshModelDoc.scope);
                InserMeshModelDoc.Parameters.AddWithValue("@role", MeshModelDoc.documentRole);

                InserMeshModelDoc.ExecuteNonQuery();
                IDMeshModelDoc = InserMeshModelDoc.LastInsertedId.ToString();
            }

            return IDMeshModelDoc;
        }
        public string CheckIfMeshModelDocExists(string LocalID, string MeshModelId)
        {
            string SqlselectDoc = "select idMeshModelDocuments from meshmodeldocuments where MeshModel_IdMeshModel = @idMeshModel AND LocalId = @LocalID";
            string DocID = null;
            MySqlCommand SelectDoc = new MySqlCommand(SqlselectDoc, conn);
            SelectDoc.Parameters.AddWithValue("@LocalId", LocalID);
            SelectDoc.Parameters.AddWithValue("@IdMeshModel", MeshModelId);


            MySqlDataReader DocReader = SelectDoc.ExecuteReader();
            if (DocReader.HasRows)
            {
                DocReader.Read();
                DocID = DocReader[0].ToString();
            }
            DocReader.Close();
            return DocID;
        }

        public string InsertMeshModelPic(image MeshModelPic, string MeshModelId)
        {

            string IDMeshModelPic = CheckIfMeshModelPicExist(MeshModelPic.idImage.ToString(), MeshModelId);
            if (IDMeshModelPic == null)
            {

                string SqlInsertMeshModelPic = "insert into meshmodelpicture" + " (Name, PictureDate, role, Author, Format, Size, Privacy, Summary,MeshModel_idMeshModel,localid) values " + " (@Name, @PictureDate, @role, @Author, @Format, @Size, @Privacy, @Summary, @idMeshModel, @localid)";
                MySqlCommand InsertMeshModelPic = new MySqlCommand(SqlInsertMeshModelPic, conn);
                InsertMeshModelPic.Parameters.AddWithValue("@Name", MeshModelPic.imageName);
                InsertMeshModelPic.Parameters.AddWithValue("@PictureDate", MeshModelPic.imageDate);
                InsertMeshModelPic.Parameters.AddWithValue("@role", MeshModelPic.documentRole);
                InsertMeshModelPic.Parameters.AddWithValue("@Author", MeshModelPic.imageAuthor);
                InsertMeshModelPic.Parameters.AddWithValue("@Format", MeshModelPic.imageFormat);
                InsertMeshModelPic.Parameters.AddWithValue("@Size", MeshModelPic.imageSize);
                InsertMeshModelPic.Parameters.AddWithValue("@Privacy", MeshModelPic.privacy);
                InsertMeshModelPic.Parameters.AddWithValue("@Summary", MeshModelPic.summary);
                InsertMeshModelPic.Parameters.AddWithValue("@Localid", MeshModelPic.idImage);
                InsertMeshModelPic.Parameters.AddWithValue("@idMeshModel", MeshModelId);

                InsertMeshModelPic.ExecuteNonQuery();
                IDMeshModelPic = InsertMeshModelPic.LastInsertedId.ToString();
            }
            return IDMeshModelPic;
        }
        public string CheckIfMeshModelPicExist(string MeshModelPicLocalID, string MeshModelId)
        {

            string picId = null;
            string sqlSelectPic = "select idMeshModelPicture  from meshmodelpicture where MeshModel_idMeshModel = @idMeshModelPic  AND LocalId = @LocalId";
            MySqlCommand SelectPic = new MySqlCommand(sqlSelectPic, conn);
            SelectPic.Parameters.AddWithValue("@idMeshModelPic", MeshModelPicLocalID);
            SelectPic.Parameters.AddWithValue("@LocalId", MeshModelId);

            MySqlDataReader pic = SelectPic.ExecuteReader();
            if (pic.HasRows)
            {
                pic.Read();
                picId = pic[0].ToString();
            }
            pic.Close();
            return picId;

        }

        public string InsertComputerSystem(string idCompExp, string[] Software)
        {
            string ComputerSystemId = null;
            //i don't check for duplicates
            //string ComputerSystemId = CheckIfComputerSystemExists(idCompExp, Software);
            foreach (string soft in Software)
            {




                if (ComputerSystemId == null)
                {
                    string sqlInsert = "Insert Into ComputerSystem" + " (CompExp_idCompExp,Software) Values" + " (@idCompExp, @Software) ";
                    MySqlCommand insert = new MySqlCommand(sqlInsert, conn);
                    insert.Parameters.AddWithValue("@idCompExp", idCompExp);
                    insert.Parameters.AddWithValue("@Software", soft);
                    insert.ExecuteNonQuery();
                    ComputerSystemId = insert.LastInsertedId.ToString();
                }
            }
            return ComputerSystemId;
        }
        public string CheckIfComputerSystemExists(string idCompExp, string Software)
        {

            string sqlSelectComputerSystem = "select idComputerSystem from ComputerSystem where CompExp_idCompExp = @idCompExp AND Software = @Software ";
            string ComputerSystemId = null;
            MySqlCommand selectComputerSystem = new MySqlCommand(sqlSelectComputerSystem, conn);
            selectComputerSystem.Parameters.AddWithValue("@idCompExp", idCompExp);
            selectComputerSystem.Parameters.AddWithValue("@Software", Software);
            selectComputerSystem.ExecuteNonQuery();
            MySqlDataReader ComputerSystem = selectComputerSystem.ExecuteReader();
            if (ComputerSystem.HasRows)
            {
                ComputerSystem.Read();
                ComputerSystemId = ComputerSystem[0].ToString();
            }

            ComputerSystem.Close();
            return ComputerSystemId;
        }

        public string InsertMeshModel_Has_Material(string idMaterial, string idMeshModel)
        {
            string IdMeshModel_Has_Material = CheckIfMeshModel_Has_MaterialExists(idMaterial, idMeshModel);

            if (IdMeshModel_Has_Material == null)
            {
                string sqlInsert = "Insert Into MeshModel_Has_Material " + " (meshmodel_idmeshmodel,material_idmaterial) Values" + " (@idMeshModel, @idMaterial) ";
                MySqlCommand insert = new MySqlCommand(sqlInsert, conn);
                insert.Parameters.AddWithValue("@idMaterial", idMaterial);
                insert.Parameters.AddWithValue("@idMeshModel", idMeshModel);
                insert.ExecuteNonQuery();
                IdMeshModel_Has_Material = insert.LastInsertedId.ToString();
            }
            return IdMeshModel_Has_Material;
        }
        public string CheckIfMeshModel_Has_MaterialExists(string idMaterial, string idMeshModel)
        {

            string sqlSelectMeshModel_Has_Material = "select idMeshModel_Has_Material from MeshModel_Has_Material where meshmodel_idmeshmodel = @idMeshModel AND material_idmaterial = @idmaterial ";
            string MeshModel_Has_MaterialId = null;
            MySqlCommand selectMeshModel_Has_Material = new MySqlCommand(sqlSelectMeshModel_Has_Material, conn);
            selectMeshModel_Has_Material.Parameters.AddWithValue("@idMeshModel", idMeshModel);
            selectMeshModel_Has_Material.Parameters.AddWithValue("@idmaterial", idMaterial);
            selectMeshModel_Has_Material.ExecuteNonQuery();
            MySqlDataReader MeshModel_Has_Material = selectMeshModel_Has_Material.ExecuteReader();
            if (MeshModel_Has_Material.HasRows)
            {
                MeshModel_Has_Material.Read();
                MeshModel_Has_MaterialId = MeshModel_Has_Material[0].ToString();
            }

            MeshModel_Has_Material.Close();
            return MeshModel_Has_MaterialId;
        }

        public int DeleteProject(string LabID)
        {
            string SqlDeleteProject = "Delete from Project where laboratory_idlaboratory = @LabID";
            MySqlCommand DeleteProject = new MySqlCommand(SqlDeleteProject, conn);
            DeleteProject.Parameters.AddWithValue("@LabID", LabID);
            int result = DeleteProject.ExecuteNonQuery();
            return result;
        }

        public int DeleteSpecimen(string projectid)
        {
            string SqlDeleteProject = "Delete from Specimen where project_idproject= @idproject";
            MySqlCommand DeleteProject = new MySqlCommand(SqlDeleteProject, conn);
            DeleteProject.Parameters.AddWithValue("@idproject", projectid);
            int result = DeleteProject.ExecuteNonQuery();
            return result;
        }
      
      

        public int DeleteDLC(string LabID)
        {
            string SqlDeleteoriginalloading = "Delete DLC from dlc,project,specimen,compexp,compexp_has_dlc where project.laboratory_idlaboratory = @labid and project.idproject = specimen.project_idproject and specimen.idspecimen  = compexp.specimen_idspecimen and compexp_has_dlc.compexp_idcompexp = compexp.idcompexp and dlc.iddlc = compexp_has_dlc.dlc_iddlc";
            MySqlCommand Deleteoriginalloading = new MySqlCommand(SqlDeleteoriginalloading, conn);
            Deleteoriginalloading.Parameters.AddWithValue("@LabID", LabID);
            int result = Deleteoriginalloading.ExecuteNonQuery();
            return result;
        }


        public int DeleteOLS(string LabID)
        {
            string SqlDeleteoriginalloading = "Delete originalloading from originalloading,nominalloading_originalloading, dlc,project,specimen,compexp,compexp_has_dlc where project.laboratory_idlaboratory = @labid and project.idproject = specimen.project_idproject and specimen.idspecimen  = compexp.specimen_idspecimen and compexp_has_dlc.compexp_idcompexp = compexp.idcompexp and dlc.iddlc = compexp_has_dlc.dlc_iddlc and nominalloading_originalloading.NominalLoading = dlc.iddlc and OriginalLoading.idOriginalLoading = nominalloading_originalloading.originalLoading";
            MySqlCommand Deleteoriginalloading = new MySqlCommand(SqlDeleteoriginalloading, conn);
            Deleteoriginalloading.Parameters.AddWithValue("@LabID", LabID);
            int result = Deleteoriginalloading.ExecuteNonQuery();
            return result;
        }


        public int DeleteInputSignal(string LabID)
        {
            string SqlDeleteInputSignal = "Delete InputSignal from InputSignal , originalloading,nominalloading_originalloading, dlc,project,specimen,compexp,compexp_has_dlc where project.laboratory_idlaboratory = @labid and project.idproject = specimen.project_idproject and specimen.idspecimen  = compexp.specimen_idspecimen and compexp_has_dlc.compexp_idcompexp = compexp.idcompexp and  compexp_has_dlc.dlc_iddlc = nominalloading_originalloading.NominalLoading and nominalloading_originalloading.EffectiveInputFile = inputsignal.idInputSignal";
            MySqlCommand Deleteoriginalloading = new MySqlCommand(SqlDeleteInputSignal, conn);
            Deleteoriginalloading.Parameters.AddWithValue("@LabID", LabID);
            int result = Deleteoriginalloading.ExecuteNonQuery();
            return result;
        }


        public int DeleteMaterial()
        {
            string SqlDeleteMaterial = "Delete from material";
            MySqlCommand DeleteRelation = new MySqlCommand(SqlDeleteMaterial, conn);
            int result = DeleteRelation.ExecuteNonQuery();
            return result;
        }
        public int DeleteDocument()
        {
            string SqlDeletedoc = "Delete from document";
            MySqlCommand DeleteRelation = new MySqlCommand(SqlDeletedoc, conn);
            int result = DeleteRelation.ExecuteNonQuery();
            return result;
        }
        public int DeletePersonnel()
        {
            string SqlDeletedoc = "Delete Personnel.* from Personnel,project_has_personnel,project where Personnel.idPersonnel = project_has_personnel.Personnel_idPersonnel and project.idproject = project_has_personnel.project_idproject";
            MySqlCommand DeleteRelation = new MySqlCommand(SqlDeletedoc, conn);
            int result = DeleteRelation.ExecuteNonQuery();
            return result;
        }

        public string InsertSpecimenPicture(string idSpecimen, image SpecImage)
        {

            string imageID = CheckIfSpecimenPictureExist(SpecImage.idImage.ToString(), idSpecimen);
            if (imageID == null)
            {
                string sqlInsertSpecimenPicture = "insert into SpecimenPictures " + " (Specimen_idSpecimen, Name, CreationDate, LocalId, role, Author, Format, Size, privacy, summary)" + " values (@idSpecimen, @Name, @CreationDate, @LocalId, @role, @Author, @Format, @Size, @privacy, @summary)";
                MySqlCommand InsertSpecimenPicture = new MySqlCommand(sqlInsertSpecimenPicture, conn);
                InsertSpecimenPicture.Parameters.AddWithValue("@idSpecimen", idSpecimen);
                InsertSpecimenPicture.Parameters.AddWithValue("@Name", SpecImage.imageName);
                InsertSpecimenPicture.Parameters.AddWithValue("@CreationDate", SpecImage.imageDate);
                InsertSpecimenPicture.Parameters.AddWithValue("@LocalId", SpecImage.idImage);

                InsertSpecimenPicture.Parameters.AddWithValue("@role", SpecImage.documentRole);
                InsertSpecimenPicture.Parameters.AddWithValue("@Author", SpecImage.imageAuthor);
                InsertSpecimenPicture.Parameters.AddWithValue("@Format", SpecImage.imageFormat);

                InsertSpecimenPicture.Parameters.AddWithValue("@Size", SpecImage.imageSize);
                InsertSpecimenPicture.Parameters.AddWithValue("@privacy", SpecImage.privacy);
                InsertSpecimenPicture.Parameters.AddWithValue("@summary", SpecImage.summary);
                
                InsertSpecimenPicture.ExecuteNonQuery();
                //returns long I have to check it
                imageID = InsertSpecimenPicture.LastInsertedId.ToString();
            }
            return imageID;

        }
        public string CheckIfSpecimenPictureExist(string ImageLocalId, string SpecimenId)
        {
            string SpecimenPictureId = null;

            string sqlSelectSpecimenPicture = "select idSpecimenPictures from SpecimenPictures where Specimen_idSpecimen = @SpecimenId AND LocalId = @LocalId";
            MySqlCommand SelectSpecimenPicture = new MySqlCommand(sqlSelectSpecimenPicture, conn);
            SelectSpecimenPicture.Parameters.AddWithValue("@SpecimenId", SpecimenId);
            SelectSpecimenPicture.Parameters.AddWithValue("@LocalId", ImageLocalId);

            MySqlDataReader SpecimenPicture = SelectSpecimenPicture.ExecuteReader();
            if (SpecimenPicture.HasRows)
            {
                SpecimenPicture.Read();
                SpecimenPictureId = SpecimenPicture[0].ToString();
            }
            SpecimenPicture.Close();
            return SpecimenPictureId;
        }

        public string InsertNominalLoading_OriginalLoading(string originalLoading, string NominalLoading, string DirectionHorizon, string EffectiveInputFileID)
        {
            string Sqlselect = "select IdNominalLoading_OriginalLoading from nominalloading_originalloading where originalLoading = @originalLoading AND NominalLoading = @NominalLoading AND DirectionHorizon = @DirectionHorizon AND EffectiveInputFile = @EffectiveInputFile";
            string Id = null;
            MySqlCommand Select = new MySqlCommand(Sqlselect, conn);
            Select.Parameters.AddWithValue("@originalLoading", originalLoading);
            Select.Parameters.AddWithValue("@NominalLoading", NominalLoading);
            Select.Parameters.AddWithValue("@DirectionHorizon", DirectionHorizon);
            Select.Parameters.AddWithValue("@EffectiveInputFile", EffectiveInputFileID);
            MySqlDataReader OriginalLoading = Select.ExecuteReader();
            if (OriginalLoading.HasRows)
            {
                OriginalLoading.Read();
                Id = OriginalLoading[0].ToString();
            }
            OriginalLoading.Close();

            if (Id == null)
            {
                //System.Console.WriteLine("relation  between original and nomianl does not exist");
                string SqlInsert = "insert into nominalloading_originalloading  " + " (originalLoading, NominalLoading,DirectionHorizon, EffectiveInputFile) values " + " (@originalLoading, @NominalLoading, @DirectionHorizon,@EfInputFile)";
                MySqlCommand Insert = new MySqlCommand(SqlInsert, conn);
                Insert.Parameters.AddWithValue("@originalLoading", originalLoading);
                Insert.Parameters.AddWithValue("@NominalLoading", NominalLoading);
                Insert.Parameters.AddWithValue("@DirectionHorizon", DirectionHorizon);
                Insert.Parameters.AddWithValue("@EfInputFile", EffectiveInputFileID);
                Insert.ExecuteNonQuery();
                Id = Insert.LastInsertedId.ToString();
            }
            return Id;

        }
        /*
        public string CheckIfNominalHasOriginalLoading(string IDNominalLoading, string IDOriginalLoading)
        {
            string Result = null;

            string sqlSelectNominalOriginalRelation = "select idNominalLoading_OriginalLoading from nominalloading_originalloading where originalLoading = @originalLoading AND  NominalLoading = @NominalLoading";
            MySqlCommand SelectNominalOriginalRelation = new MySqlCommand(sqlSelectNominalOriginalRelation, conn);
            SelectNominalOriginalRelation.Parameters.AddWithValue("@originalLoading", IDOriginalLoading);
            SelectNominalOriginalRelation.Parameters.AddWithValue("@NominalLoading", IDNominalLoading);


            MySqlDataReader NominalOriginalRelationReader = SelectNominalOriginalRelation.ExecuteReader();
            if (NominalOriginalRelationReader.HasRows)
            {
                NominalOriginalRelationReader.Read();
                Result = NominalOriginalRelationReader[0].ToString();
            }
            NominalOriginalRelationReader.Close();
            return Result;

        }
        */


        public void UpdateSearch()
        {



            //System.Console.WriteLine("relation  between original and nomianl does not exist");


            string SqlInsert = "delete from search ;";

            MySqlCommand Insert = new MySqlCommand(SqlInsert, conn);
            Insert.ExecuteNonQuery();

            Insert.CommandText = "insert into search  (Category,Keyword,ProjectID,ParentID,ProjectTitle,abstract,startdate,enddate,type,labid,labname) (select 'resource', location.resource , project.idProject,project.idProject,project.title, Project.reason,project.startdate, project.enddate, 'Project',project.laboratory_idlaboratory,laboratory.LongName from laboratory, Location, project_has_location, Project where laboratory.idlaboratory =project.laboratory_idlaboratory and project_has_location.Project_idProject = Project.idProject AND project_has_location.Location_idLocation = Location.idLocation and location.resource is not null and location.resource != ' ');";
            Insert.ExecuteNonQuery();

            Insert.CommandText = "insert into search  (Category,Keyword,ProjectID,ParentID,ProjectTitle,abstract,startdate,enddate,type,labid,labname) (select 'location', location.location , project.idProject,project.idProject,project.title, Project.reason ,project.startdate,project.enddate , 'Project',project.laboratory_idlaboratory,laboratory.LongName from laboratory,Location, project_has_location, Project where laboratory.idlaboratory =project.laboratory_idlaboratory and project_has_location.Project_idProject = Project.idProject AND project_has_location.Location_idLocation = Location.idLocation and location.location is not null and location.location != ' ');";
            Insert.ExecuteNonQuery();

            Insert.CommandText = "insert into search  (Category,Keyword,ProjectID,ParentID,ProjectTitle,abstract,startdate,enddate,type,labid,labname) (select 'location', location.location , project.idProject,project.idProject,project.title, Project.reason ,project.startdate,project.enddate, 'Project',project.laboratory_idlaboratory,laboratory.LongName   from laboratory,Location, project_has_location, Project where laboratory.idlaboratory =project.laboratory_idlaboratory and project_has_location.Project_idProject = Project.idProject AND project_has_location.Location_idLocation = Location.idLocation and location.location is not null and location.location != ' ');";
            Insert.ExecuteNonQuery();

            Insert.CommandText = "insert into search  (Category,Keyword,ProjectID,ParentID,ProjectTitle,abstract,startdate,enddate,type,labid,labname) (select 'Infrastructure',location.Infrastructure , project.idProject, project.idProject, project.title , Project.reason ,project.startdate,project.enddate  , 'Project',project.laboratory_idlaboratory,laboratory.LongName   from laboratory,Location, project_has_location, Project where laboratory.idlaboratory =project.laboratory_idlaboratory and project_has_location.Project_idProject = Project.idProject AND project_has_location.Location_idLocation = Location.idLocation and location.Infrastructure is not null and location.Infrastructure  != ' ');";
            Insert.ExecuteNonQuery();

            Insert.CommandText = "insert into search (Category,Keyword,ProjectID,ParentID,ProjectTitle,abstract,startdate,enddate,type,labid,labname) (select 'ResearchArea', project.MainFocus, project.idProject, project.idProject, project.title , Project.reason ,project.startdate,project.enddate, 'Project',project.laboratory_idlaboratory,laboratory.LongName   from laboratory,Project where laboratory.idlaboratory =project.laboratory_idlaboratory and project.MainFocus is not null and project.MainFocus != ' ');";
            Insert.ExecuteNonQuery();

            Insert.CommandText = "insert into search (Category,Keyword,ProjectID,ParentID,ProjectTitle,abstract,startdate,enddate,type,labid,labname) (select 'acronym', project.acronym, project.idProject, project.idProject, project.title , Project.reason ,project.startdate,project.enddate, 'Project',project.laboratory_idlaboratory,laboratory.LongName   from laboratory,project  where laboratory.idlaboratory =project.laboratory_idlaboratory and acronym is not null and acronym != ' ');";
            Insert.ExecuteNonQuery();

            Insert.CommandText = "insert into search (Category,Keyword,ProjectID,ParentID,ProjectTitle,abstract,startdate,enddate,type,labid,labname) (select 'investigator', personnel.name, project.idProject, project.idProject, project.title, Project.reason ,project.startdate,project.enddate, 'Project',project.laboratory_idlaboratory,laboratory.LongName   from laboratory,project,personnel,project_has_personnel where laboratory.idlaboratory =project.laboratory_idlaboratory and project.idproject = project_has_personnel.project_idproject and project_has_personnel.Personnel_idPersonnel = personnel.idpersonnel and personnel.name is not null and personnel.name != ' ');";
            Insert.ExecuteNonQuery();

            Insert.CommandText = "insert into search (Category,Keyword,ProjectID,ParentID,ProjectTitle,Abstract, startdate, enddate, SpecimenID,type,labid,labname) (select 'structural element', structuralelement.type, project.idproject, specimen.idspecimen, project.title,project.reason, project.StartDate, project.enddate, Specimen.idspecimen,'Specimen',project.laboratory_idlaboratory,laboratory.LongName   from laboratory,structuralElement, project,specimen where laboratory.idlaboratory =project.laboratory_idlaboratory and project.idproject = specimen.project_idproject and specimen.idspecimen = structuralelement.specimen_idspecimen and structuralelement.type is not null and structuralelement.type != ' ');";
            Insert.ExecuteNonQuery();

            Insert.CommandText = "insert into search (Category,Keyword,ProjectID,ParentID,ProjectTitle,Abstract, startdate, enddate, SpecimenID,type,labid,labname) (select distinct 'StructuralMaterial', material.name, project.idproject, specimen.idspecimen, project.title,project.reason, project.StartDate, project.enddate, specimen.idspecimen,'Specimen',project.laboratory_idlaboratory,laboratory.LongName    from laboratory,material, project,structuralelement_has_material,structuralelement,specimen WHERE laboratory.idlaboratory =project.laboratory_idlaboratory and material.idmaterial = structuralelement_has_material.Material_idMaterial AND structuralelement_has_material.StructuralElement_idStructuralElement = structuralelement.idStructuralElement AND structuralelement.Specimen_idSpecimen = specimen.idspecimen AND project.idproject = specimen.project_idproject and material.name is not null  and material.name != ' ');";
            Insert.ExecuteNonQuery();

            Insert.CommandText = " insert into search (Category,Keyword,ProjectID,ParentID,ProjectTitle,Abstract, startdate, enddate, SpecimenID,type,labid,labname) ( select distinct 'similitude',  similitude.scaledPropertyName, project.idproject , specimen.idspecimen, project.title,project.reason, project.StartDate, project.enddate, specimen.idspecimen,'Specimen',project.laboratory_idlaboratory,laboratory.LongName  from laboratory,similitude,specimen,project where laboratory.idlaboratory =project.laboratory_idlaboratory and project.idproject = specimen.project_idproject AND specimen.idspecimen = similitude.specimen AND  similitude.scaledPropertyName is not null and similitude.scaledPropertyName != ' ' );";
            Insert.ExecuteNonQuery();

            Insert.CommandText = "insert into search (Category,Keyword,ProjectID,ParentID,ProjectTitle,Abstract, startdate, enddate, SpecimenID,type,labid,labname) ( select distinct 'specimen pictures',  specimenpictures.Type, project.idproject , specimen.idspecimen, project.title,project.reason, project.StartDate, project.enddate, specimen.idspecimen,'Specimen' ,project.laboratory_idlaboratory,laboratory.LongName from laboratory,project, specimenpictures,specimen where laboratory.idlaboratory =project.laboratory_idlaboratory and project.idproject = specimen.project_idproject  AND specimenpictures.specimen_idspecimen = specimen.idspecimen and specimenpictures.Type is not null and specimenpictures.Type  != ' ');";
            Insert.ExecuteNonQuery();

            Insert.CommandText = "insert into search (Category,Keyword,ProjectID,ParentID,ProjectTitle,Abstract, startdate, enddate, specimenid, compexpid,type,labid,labname) ( SELECT distinct 'exptype', compexp.ExpCompType, project.idproject, compexp.idcompexp, project.title,project.reason, project.StartDate, project.enddate, specimen.idspecimen,compexp.idcompexp, compexp.type ,project.laboratory_idlaboratory,laboratory.LongName FROM laboratory,compexp,project,specimen WHERE laboratory.idlaboratory =project.laboratory_idlaboratory and project.idproject = specimen.project_idproject AND specimen.idspecimen = compexp.specimen_idspecimen AND Compexp.type= 'Experiment' AND compexp.ExpCompType is not null and compexp.ExpCompType != ' ' );";
            Insert.ExecuteNonQuery();

            Insert.CommandText = "insert into search (Category,Keyword,ProjectID,ParentID,ProjectTitle,Abstract, startdate, enddate, specimenid, compexpid,type,labid,labname) (select distinct 'OLS_nature',originalloading.nature, project.idproject, compexp.idcompexp, project.title,project.reason, project.StartDate, project.enddate, specimen.idspecimen,compexp.idcompexp, compexp.type,project.laboratory_idlaboratory,laboratory.LongName   from laboratory,project, originalloading, dlc, nominalloading_originalloading, specimen,compexp, compexp_has_dlc WHERE laboratory.idlaboratory =project.laboratory_idlaboratory and Originalloading.idOriginalloading = nominalloading_originalloading.originalloading AND nominalloading_originalloading.nominalloading = dlc.iddlc AND compexp_has_dlc.dlc_iddlc = dlc.iddlc and compexp_has_dlc.compexp_idcompexp = compexp.idcompexp and CompExp.specimen_idspecimen = specimen.idspecimen AND project.idproject = specimen.project_idproject and originalloading.nature is not null and originalloading.nature != ' ');";
            Insert.ExecuteNonQuery();

            Insert.CommandText = " insert into search (Category,Keyword,ProjectID,ParentID,ProjectTitle,Abstract, startdate, enddate, specimenid, compexpid,type,labid,labname) ( select distinct 'OLS_Source',originalloading.source, project.idproject, compexp.idcompexp, project.title,project.reason, project.StartDate, project.enddate, specimen.idspecimen,compexp.idcompexp, compexp.type,project.laboratory_idlaboratory,laboratory.LongName     from laboratory,project, originalloading, dlc, nominalloading_originalloading, specimen,compexp , compexp_has_dlc WHERE laboratory.idlaboratory =project.laboratory_idlaboratory and Originalloading.idOriginalloading = nominalloading_originalloading.originalloading AND nominalloading_originalloading.nominalloading = dlc.iddlc AND compexp_has_dlc.dlc_iddlc = dlc.iddlc and compexp_has_dlc.compexp_idcompexp = compexp.idcompexp AND CompExp.specimen_idspecimen = specimen.idspecimen AND project.idproject = specimen.project_idproject and originalloading.source is not null and originalloading.source !=  ' ');";
            Insert.ExecuteNonQuery();

            Insert.CommandText = "insert into search (Category,Keyword,ProjectID,ParentID,ProjectTitle,Abstract, startdate, enddate, specimenid, compexpid,type,labid,labname) ( select distinct 'CompType',compexp.ExpCompType, project.idproject , compexp.idcompexp, project.title,project.reason, project.StartDate, project.enddate, specimen.idspecimen,compexp.idcompexp, compexp.type ,project.laboratory_idlaboratory,laboratory.LongName    FROM laboratory,compexp,project,specimen WHERE laboratory.idlaboratory =project.laboratory_idlaboratory and project.idproject = specimen.project_idproject AND specimen.idspecimen = compexp.specimen_idspecimen AND Compexp.type= 'Computation' AND compexp.ExpCompType is not null and compexp.ExpCompType != ' ');";
            Insert.ExecuteNonQuery();

            Insert.CommandText = " insert into search (Category,Keyword,ProjectID,ParentID,ProjectTitle,Abstract, startdate, enddate, specimenid, compexpid,type,labid,labname) ( select distinct 'SymmetryType',MeshModel.SymmetryType, project.idproject , compexp.idcompexp, project.title,project.reason, project.StartDate, project.enddate, specimen.idspecimen,compexp.idcompexp, compexp.type ,project.laboratory_idlaboratory,laboratory.LongName    from laboratory,project,specimen,compexp,meshmodel WHERE laboratory.idlaboratory =project.laboratory_idlaboratory and project.idproject = specimen.project_idproject AND specimen.idspecimen = compexp.specimen_idspecimen AND CompExp.Type= 'Computation' AND meshmodel.CompExp_idcompexp = compexp.idcompexp and MeshModel.SymmetryType is not null and MeshModel.SymmetryType != ' ');";
            Insert.ExecuteNonQuery();

            Insert.CommandText = " insert into search (Category,Keyword,ProjectID,ParentID,ProjectTitle,Abstract, startdate, enddate, specimenid, compexpid,type,labid,labname) ( select distinct 'NonLinearity',MeshModel.NonLinearity, project.idproject , compexp.idcompexp, project.title,project.reason , project.StartDate, project.enddate, specimen.idspecimen,compexp.idcompexp, compexp.type,project.laboratory_idlaboratory,laboratory.LongName    from laboratory,project,specimen,compexp,meshmodel WHERE laboratory.idlaboratory =project.laboratory_idlaboratory and project.idproject = specimen.project_idproject AND specimen.idspecimen = compexp.specimen_idspecimen AND CompExp.Type= 'Computation' AND meshmodel.CompExp_idcompexp = compexp.idcompexp and  MeshModel.NonLinearity is not null and MeshModel.NonLinearity != ' ');";
            Insert.ExecuteNonQuery();

            Insert.CommandText = "insert into search (Category,Keyword,ProjectID,ParentID,ProjectTitle,Abstract, startdate, enddate, specimenid, compexpid,type,labid,labname) ( select distinct 'CompSoft', computersystem.Software, project.idproject , compexp.idcompexp, project.title,project.reason , project.StartDate, project.enddate, specimen.idspecimen,compexp.idcompexp, compexp.type ,project.laboratory_idlaboratory,laboratory.LongName   from laboratory,project,computerSystem,compexp,specimen WHERE laboratory.idlaboratory =project.laboratory_idlaboratory and project.idproject = specimen.project_idproject AND specimen.idspecimen = compexp.specimen_idspecimen AND computersystem.CompExp_idCompExp = compexp.idcompexp and computersystem.Software is not null and computersystem.Software != ' ');";
            Insert.ExecuteNonQuery();

            Insert.CommandText = " insert into search (Category,Keyword,ProjectID,ParentID,ProjectTitle,Abstract, startdate, enddate, specimenid, compexpid, signalid,type,labid,labname) ( Select distinct 'PhysicalQ', SignalResult.PhysicalQ, project.idproject, signalresult.idSignalResult, project.title,project.reason, project.StartDate, project.enddate ,specimen.idspecimen,compexp.idcompexp,signalresult.idsignalresult,'Signal',project.laboratory_idlaboratory,laboratory.LongName   from laboratory,project, specimen , compexp, signalresult WHERE laboratory.idlaboratory =project.laboratory_idlaboratory and project.idproject = specimen.project_idproject AND specimen.idspecimen = compexp.specimen_idspecimen AND signalresult.CompExp_idCompExp = compexp.idcompexp and SignalResult.PhysicalQ is not null and SignalResult.PhysicalQ != ' '  );";
            Insert.ExecuteNonQuery();





        }
    }
    public class Logger 
    {
        //private string operation;
        //private string timestamp;
        //private string LabID;
        //private string result;
        private MySqlConnection conn = new MySqlConnection();
        private string connectionStr = "server=localhost;user=root;database=newseriesserver;port=3306;password=root;";

        
            
        

        public void CreateLogIntro(string DbOp,string CrTimestamp,string CrId,string CrTable)
        {
            conn.ConnectionString = connectionStr;
            conn.Open();

            string sqlInsert = "Insert Into loginfo" + " (dbOperation, logtime, OperationId, TableAffected) Values" + " (@Operation, @logtime, @OperationId, @TableAffected) ";
            //string sqlInsert = "Insert Into laboratory" + " (Name, ip, Interv,LastUpdateTime,TimeDif) Values" + " (@Name,@ip,@interval,@LastUpdateTime,@TimeDif) ";
            MySqlCommand insert = new MySqlCommand(sqlInsert, conn);
            insert.Parameters.AddWithValue("@Operation", DbOp);
            insert.Parameters.AddWithValue("@logtime", CrTimestamp);
            insert.Parameters.AddWithValue("@OperationId", CrId);
            insert.Parameters.AddWithValue("@TableAffected", CrTable);
            //insert.Parameters.AddWithValue("@Result", CrResult);
           
            insert.ExecuteNonQuery();
            //check if I can run it without open and closing it
            conn.Close();
           

        }
    }


}
