using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ViDi2;
// You may have to install VisionPro application to import the library.
using ViDi2.VisionPro;
using ViDi2.VisionPro.Records;
using IControl = ViDi2.Runtime.IControl;
using IStream = ViDi2.Runtime.IStream;
using IWorkspace = ViDi2.Runtime.IWorkspace;

using ViDi2.Runtime;
using ViDi2.Training;
using System.Text.RegularExpressions;
using Google.Protobuf.WellKnownTypes;
//using ViDi2.UI;



namespace Example.VisionPro.Integration
{
    /// <summary>
    /// Describes how to use serialized deep learning and VisionPro
    /// tools to fixture deep learning tools, run them, and produce
    /// VisionPro compatible graphics.
    /// </summary>
    internal class VisionProIntegration : INotifyPropertyChanged
    {
        private IControl _control;
        /// <summary>
        /// Gets or sets the main control providing access to the library
        /// </summary>
        public IControl Control
        {
            get { return _control; }
            set
            {
                _control = value;
                RaisePropertyChanged(nameof(Control));
            }
        }

        private IWorkspace _workspace;
        /// <summary>
        /// Gets or sets the current workspace
        /// </summary>
        public IWorkspace Workspace
        {
            get { return _workspace; }
            set
            {
                _workspace = value;
                Stream = _workspace.Streams.First();
                RaisePropertyChanged(nameof(Workspace));
                RaisePropertyChanged(nameof(IsReadyToProcess));
            }
        }

        private IStream _stream;
        /// <summary>
        /// Gets or sets the current stream
        /// </summary>
        public IStream Stream
        {
            get { return _stream; }
            set
            {
                _stream = value;
                RaisePropertyChanged(nameof(Stream));
            }
        }

        private string _workspaceName;
        public string WorkspaceName
        {
            get { return _workspaceName; }
            set
            {
                _workspaceName = value;
                RaisePropertyChanged(nameof(WorkspaceName));
            }
        }

        private string _toolBlockName;
        public string ToolBlockName
        {
            get { return _toolBlockName; }
            set
            {
                _toolBlockName = value;
                RaisePropertyChanged(nameof(ToolBlockName));
            }
        }

        private IList<LabeledViewRecord> _views;
        /// <summary>
        /// A collection of ViewRecords produced by the last
        /// execution of the deep learning tools. Each record is labelled
        /// with the tool that owns the view, and the order the view was
        /// found in.
        /// </summary>
        public IList<LabeledViewRecord> Views
        {
            get
            {
                return _views;
            }
            set
            {
                _views = value;
                RaisePropertyChanged(nameof(Views));
                RaisePropertyChanged(nameof(HasViews));
                RaisePropertyChanged(nameof(IsReadyToProcess));
            }
        }

        public bool HasViews => Views != null && Views.Any();

        private ViewRecord _selectedViewRecord;
        public ViewRecord SelectedViewRecord
        {
            get
            {
                return _selectedViewRecord;
            }
            set
            {
                _selectedViewRecord = value;
                RaisePropertyChanged(nameof(SelectedViewRecord));
                RaisePropertyChanged(nameof(HasSelectedViewRecord));
            }
        }

        public bool HasSelectedViewRecord => SelectedViewRecord != null;

        public bool IsReadyToProcess => ToolBlock != null && Workspace != null && !HasViews;

        /// <summary>
        /// A configuration that records exactly which graphics
        /// (per view) are hidden in a given record. The configuration
        /// can be used to hide the same graphics in the same views
        /// in another record.
        /// </summary>
        public RecordConfiguration RecordConfig { get; set; }

        private CogToolBlock _toolBlock;
        /// <summary>
        /// A tool block containing a PMAlign tool used to
        /// input multiple fixtures into a series of deep learning
        /// tools.
        /// </summary>
        public CogToolBlock ToolBlock {
            get { return _toolBlock; }
            set
            {
                _toolBlock = value;
                RaisePropertyChanged(nameof(ToolBlock));
                RaisePropertyChanged(nameof(IsReadyToProcess));
            }
        }

        /// <summary>
        /// Runs all of the tools against the given image,
        /// using the results of the VisionPro tools to fixture
        /// the Deep Learning tools, and returning the image
        /// and all graphics as a single record.
        /// </summary>
        /// <param name="image"> The image to process. </param>
        /// <returns> A single record with the input image and graphics for all tools. </returns>
        public ICogRecord RunTools(IImage image)
        {
            // Create a sample to use when processing Deep Learning tools or getting results
            var sample = Stream.CreateSample(image);
            var cogImage = ImageExtensions.ToCogImage(image);

            // Run all of the tools
     //       RunVisionProToolsAndSetUpFixture(cogImage, sample);
            var toolRecords = RunDeepLearningToolsAndGetRecords(image, sample);

          


            // Add the VPro records
            // toolRecords.Add(ToolBlock.CreateLastRunRecord());


            ICogRecord returnRecord = CreateCombinedRecord(toolRecords, cogImage);

            if (RecordConfig == null)
            {
                RecordConfig = new RecordConfiguration(returnRecord);
            }
            // Hide the records that were previously hidden, if any were hidden
            RecordConfig.ApplyRecordConfiguration(returnRecord);



            // Extract view records so individual views can be hidden
            Views = ExtractViewRecords(toolRecords);


            if (HasViews)
            {
                SelectedViewRecord = Views.First().Record;
            }

            // Combine the image and results into a single record
            return returnRecord;
        }

        /// <summary>
        /// Runs the VisionPro tool block, using the image as
        /// input and using the outputs to fixture
        /// the first Deep Learning tool in the current
        /// stream.
        /// </summary>
        /// <param name="cogImage"> The image to process. </param>
        /// <param name="sample"> The sample created from the image being processed. </param>
        private void RunVisionProToolsAndSetUpFixture(ICogImage cogImage, ISample sample)
        {
            // Pass the image as input to the tool block and run the VPro tools
            ToolBlock.Inputs[0].Value = cogImage;
            ToolBlock.Run();

            // Get the poses found by the PMAlign tool
            var poses = new List<CogTransform2DLinear>();
            foreach (CogToolBlockTerminal toolBlockOutput in ToolBlock.Outputs)
            {
                poses.Add(toolBlockOutput.Value as CogTransform2DLinear);
            }

           // // Fixture the first tool using the poses
           // IManualRegionOfInterest manualROI = Stream.Tools.First().RegionOfInterest as IManualRegionOfInterest;
           //
           // //it only works in external mode
           // manualROI.Parameters.Mode = ManualRegionOfInterestMode.External;
           //
           // foreach (var pose in poses)
           //     manualROI.AddView(sample.Name, new Size(0.0, 0.0), pose.ToMatrixTransform());
        }

        /// <summary>
        /// Runs each tool in the current Stream,
        /// processing the given image and converting
        /// the resulting marking into an ICogRecord.
        /// </summary>
        /// <param name="image"> The image to process. </param>
        /// <param name="sample"> The sample to use to process each tool. </param>
        /// <returns> A list of records, each containing the graphics for a different tool. </returns>
        private List<ICogRecord> RunDeepLearningToolsAndGetRecords(IImage image, ISample sample)
        {
            // Process each Deep Learning tool, converting the results to ICogRecords
            var toolRecords = new List<ICogRecord>();


            SortedList<string, string> GetToolsName = new SortedList<string, string>();
            foreach (var toolitem in Stream.Tools)
            {
                GetToolsName.Add(toolitem.Name.ToString(), toolitem.Type.ToString());
                GetToolName(GetToolsName, Stream.Tools[toolitem.Name].Children);
            }



            ///한번에 Process 하기. 
            ISample samples = Stream.Process(image);
           
            foreach (var ToolType in GetToolsName)
            {
                switch (ToolType.Key)
                {
                    case "Blue":
                    case "Locate":
                        CogGraphicCollection graphicCollectionBlue = new CogGraphicCollection();

                        var MarkingBlue = samples.Markings[ToolType.Key] as ViDi2.IBlueMarking;
                        toolRecords.Add(CreateToolRecordFromMarking(MarkingBlue, image, ToolType.Key));
         
                        foreach (var view in MarkingBlue.Views)
                        {
                            System.Console.WriteLine($"This View Features Count: \" {view.Features.Count.ToString()} ");
                            foreach (var match in view.Features)
                            {
                                CogGraphicLabel graphicLabel = new CogGraphicLabel();
                                graphicLabel.BackgroundColor = CogColorConstants.White;
                                graphicLabel.Text = "Index:" + match.Name + ", Score:";
                                graphicLabel.Text += (match.Score * 100.0).ToString("00");

                                if (false)
                                {
                                    graphicLabel.X = match.Position.X;
                                    graphicLabel.Y = match.Position.Y;
                                }
                                else
                                {
                                    graphicLabel.X = match.Position.X - (match.Size.Width  / 2);
                                    graphicLabel.Y = match.Position.Y - (match.Size.Height / 2);
                                }
                                graphicCollectionBlue.Add(graphicLabel);
                            }
                        }
                        toolRecords[toolRecords.Count() -1].SubRecords.Add(new Cognex.VisionPro.Implementation.CogRecord("match.Score", typeof(CogGraphicCollection), CogRecordUsageConstants.Result, false, graphicCollectionBlue, "graphicCollectionBlue"));

                        #region Blue
                        //stream.Tools["localize"].Process(image2);

                        //ViDi2.Runtime.ITool blueTool = Stream.Tools[ToolType];
                        //ViDi2.IBlueMarking blueMarking = blueTool.Process(image).Markings[blueTool.Name] as ViDi2.IBlueMarking;

                        //SampleViewerViewModel.Sample.Process(blueTool);
                        //ViDi2.IBlueMarking blueMarking = SampleViewerViewModel.Sample.Markings[blueTool.Name] as ViDi2.IBlueMarking;


                        //foreach (var view in blueMarking.Views)
                        //{
                        //    System.Console.WriteLine($"This View Features Count: \" {view.Features.Count.ToString()} ");
                        //    foreach (var match in view.Features)
                        //    {
                        //        //System.Console.WriteLine($"This model says \" {(match as ViDi2.IReadModelMatch).FeatureString} \" with a score of {match.Score * 100.0}");
                        //        System.Console.WriteLine($" with a score of {match.Score * 100.0}");
                        //    }
                        //}
                        #endregion
                        break;


                    case "RedHighDetail":
                    case "용액":
                    //case "카본":

                        CogGraphicCollection graphicCollectionRed = new CogGraphicCollection();

                            var MarkingRed = samples.Markings[ToolType.Key] as ViDi2.IRedMarking;
                            toolRecords.Add(CreateToolRecordFromMarking(MarkingRed, image, ToolType.Key));

                            foreach (ViDi2.IRedView view in MarkingRed.Views)
                            {
                                System.Console.WriteLine($"This view has a score of {view.Score}");
                                foreach (ViDi2.IRegion region in view.Regions)
                                {
                                    CogGraphicLabel graphicLabel = new CogGraphicLabel();
                                    graphicLabel.BackgroundColor = CogColorConstants.DarkRed;
                                    graphicLabel.Text = (region.Area).ToString("00");

                                    if (false) 
                                    {
                                        graphicLabel.X = region.Center.X + view.Pose.OffsetX;
                                        graphicLabel.Y = region.Center.Y + view.Pose.OffsetY;
                                    }
                                    else
                                    {
                                        graphicLabel.X = view.Pose.OffsetX;
                                        graphicLabel.Y = view.Pose.OffsetY + region.Center.Y;
                                    }
                                    graphicCollectionRed.Add(graphicLabel);

                                    CogPolygon cogPolygon = new CogPolygon();
                                    cogPolygon.Color = CogColorConstants.Yellow;
                                    foreach (var point in region.Outer.Select((value,index)=>(value,index)))
                                    {
                                        cogPolygon.AddVertex(point.value.X + view.Pose.OffsetX, point.value.Y + view.Pose.OffsetY, point.index);
                                    }
                                    graphicCollectionRed.Add(cogPolygon);
                                }
                            }
                            toolRecords[toolRecords.Count() - 1].SubRecords.Add(new Cognex.VisionPro.Implementation.CogRecord("TrainRegion", typeof(CogGraphicCollection), CogRecordUsageConstants.Result, false, graphicCollectionRed, "graphicCollectionRed"));

                            #region Redtool
                            //ViDi2.Runtime.ITool redTool = Stream.Tools["Analyze"];
                            // We can process the red tool, it won't reprocess the blue tool since the result
                            // is up to date.


                            //ViDi2.IRedMarking redMarking = redTool.Process(image).Markings[redTool.Name] as ViDi2.IRedMarking;
                            //         SampleViewerViewModel.Sample.AddImage(image2);
                            //         SampleViewerViewModel.Sample.Process(redTool);

                            //ViDi2.IRedMarking redMarking = SampleViewerViewModel.Sample.Markings[redTool.Name] as ViDi2.IRedMarking;

                            //foreach (ViDi2.IRedView view in redMarking.Views)
                            //{
                            //    //System.Console.WriteLine($"This view has a score of {view.Score}");
                            //    //foreach (ViDi2.IRegion region in view.Regions)
                            //    //{
                            //    //    Console.WriteLine($"This region has a score of {region.Covers}");
                            //    //}
                            //}
                            #endregion
                        
                        break;
                }
            }

            return toolRecords;
        }

        private void GetToolName(SortedList<string, string> ListToolsName, ViDi2.IToolList<ViDi2.Runtime.ITool> _Children)
        {

            try
            {
                foreach (var Tool in _Children)
                {
                    ListToolsName.Add(Tool.Name.ToString(), Tool.Type.ToString());

                    try
                    {
                        this.GetToolName(ListToolsName, _Children[Tool.Name].Children);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// Converts deep learning results/graphics for a particular
        /// family of deep learning tool into the equivalent VisionPro
        /// representation of those graphics: a record.
        /// </summary>
        /// <param name="marking"> The deep learning results to convert. </param>
        /// <param name="image"> The input image. </param>
        /// <param name="toolName"> The name of the tool that produced the marking. </param>
        /// <returns> A record with equivalent graphics. </returns>
        private ICogRecord CreateToolRecordFromMarking(IMarking marking, IImage image, string toolName)
        {
            ICogRecord toolRecord = null;

            if (marking is IBlueMarking)
            {
                toolRecord = new BlueToolRecord((IBlueMarking)marking, image, toolName);
            }
            else if (marking is IRedMarking)
            {
                var redToolRecord = new RedToolRecord((IRedMarking)marking, image, toolName);

                // Show the heatmap graphics to make defects more obvious
                if (redToolRecord.HasHeatMap())
                {
                    redToolRecord.SetHeatMapGraphicsVisibility(true);
                }

                toolRecord = redToolRecord;
            }
            else if (marking is IGreenMarking)
            {
                toolRecord = new GreenToolRecord((IGreenMarking)marking, image, toolName);
            }

            return toolRecord;
        }


        /// <summary>
        /// Creates a collection of the ViewRecords created by
        /// an execution of deep learning tools. Each record is given
        /// a name of the following format: "{Name of Tool} - View{#}"
        /// where the first view is View0, the second is View1, and so on.
        /// </summary>
        /// <param name="toolRecords"> A collection of IToolRecords generated by deep learning tools. </param>
        /// <returns>A list containing all of the ViewRecords, each assigned a name. </returns>
        private IList<LabeledViewRecord> ExtractViewRecords(List<ICogRecord> toolRecords)
        {
            IList<LabeledViewRecord> labeledViewRecords = new List<LabeledViewRecord>();

            foreach (IToolRecord toolRecord in toolRecords.Where(record => record is IToolRecord))
            {
                var toolName = toolRecord.Annotation;
                var viewIndex = 0;

                foreach (var viewRecord in toolRecord.Views)
                {
                    labeledViewRecords.Add(new LabeledViewRecord($"{toolName} - View{viewIndex++}", viewRecord));
                }
            }

            return labeledViewRecords;
        }



        /// <summary>
        /// Combine a list of records into a single record.
        /// The Content of this record will be the input image.
        /// The SubRecords of this record will be the records
        /// created from each tool.
        /// </summary>
        /// <param name="records"> The records to combine and the subrecords of the resulting record. </param>
        /// <param name="image"> The content of the resulting record. </param>
        /// <returns> A single record that owns the image to display and all result graphics. </returns>
        private ICogRecord CreateCombinedRecord(IList<ICogRecord> records, ICogImage image)
        {
            var containerRecord = new Record(image, "Results");

            foreach (var cogRecord in records)
            {
                containerRecord.SubRecords.Add(cogRecord);
            }

            return containerRecord;
        }

        /// <summary>
        /// Tests if the provided CogToolBlock can provide fixturing for deep learning tools.
        /// An acceptable CogToolBlock takes one ICogImage and outputs 1 to many CogTransform2DLinear
        /// to fixture the deep learning tool views.
        /// </summary>
        /// <param name="toolBlock"> The tool block to test. </param>
        /// <returns> Whether or not the tool block can be used to fixture a deep learning tool. </returns>
        public bool IsToolBlockValid(CogToolBlock toolBlock)
        {
            bool hasOneICogImageInput = toolBlock.Inputs.Count == 1
                                        && toolBlock.Inputs[0].ValueType == typeof(ICogImage);

            bool allOutputsAreTransforms = toolBlock.Outputs.Any() &&
                                           toolBlock.Outputs.All(output => output.ValueType
                                                                           == typeof(CogTransform2DLinear));

            return hasOneICogImageInput && allOutputsAreTransforms;
        }

        /// <summary>
        /// Tests if the provided runtime workspace has any tools to run.
        /// The fixturing tool block will be used to fixture the first tool of the first stream.
        /// </summary>
        /// <param name="workspace"> The workspace to test. </param>
        /// <returns> Whether or not the workspace has any deep learning tools to run. </returns>
        public bool IsRuntimeWorkspaceValid(IWorkspace workspace)
        {
            return workspace.Streams.Any() && workspace.Streams.First().Tools.Any();
        }

        private void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
