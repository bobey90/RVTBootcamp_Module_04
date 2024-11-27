using Autodesk.Revit.UI;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RVTBootcamp_Module_04
{
    internal class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication app)
        {
            //1. Create Tab

            string tabName = "My Tab";
            app.CreateRibbonTab(tabName);

            //1b. Create tabe and panel - safer version

            try
            {
                app.CreateRibbonTab(tabName);

            }
            catch (Exception error)  
            {
                Debug.Print("Tab already exist. Using existing panel.");
                Debug.Print(error.Message);
            }

            //2. Create Panel

            string panelName1 = "Test Panel";
            string panelName2 = "Test Panel 2";
            string panelName3 = "Test Panel 3";



            RibbonPanel panel = app.CreateRibbonPanel(tabName, panelName1);
            RibbonPanel panel2 = app.CreateRibbonPanel(panelName2);
            RibbonPanel panel3 = app.CreateRibbonPanel(panelName3);

            //2a. Get existing panel

            List<RibbonPanel> panelList = app.GetRibbonPanels();
            List<RibbonPanel> panelList2 = app.GetRibbonPanels(tabName);

            //2b. Create/Get panel method - Safe method

            RibbonPanel panel4 = CreateGetPanel(app, tabName, panelName1);

            //3. Create button data

            PushButtonData buttonData1 = new PushButtonData(
                "button1",
                "Command 1",
                Assembly.GetExecutingAssembly().Location,
                "RVTBootcamp_Module_04.Command1");

            PushButtonData buttonData2 = new PushButtonData(
                "button2",
                "Button\rCommand 2",
                Assembly.GetExecutingAssembly().Location,
                "RVTBootcamp_Module_04.Command2");


            //4. Add ToolTips
            buttonData1.ToolTip = "This is command 1";
            buttonData2.ToolTip = "This is command 2";



            //5. convert images

            Bitmap myBitmap = Properties.Resources.Green_32; // Example of loading a Bitmap from resources
            byte[] byteArray = ConvertBitmapToByteArray(myBitmap);

            Bitmap myBitmap2 = Properties.Resources.Green_16; // Example of loading a Bitmap from resources
            byte[] byteArray2 = ConvertBitmapToByteArray(myBitmap2);

            Bitmap myBitmap3 = Properties.Resources.Blue_32; // Example of loading a Bitmap from resources
            byte[] byteArray3 = ConvertBitmapToByteArray(myBitmap3);

            Bitmap myBitmap4 = Properties.Resources.Blue_16; // Example of loading a Bitmap from resources
            byte[] byteArray4 = ConvertBitmapToByteArray(myBitmap4);

            //5a. Add images

            buttonData1.LargeImage = ConvertToImageSource(byteArray);
            buttonData1.Image = ConvertToImageSource(byteArray2);
            buttonData2.LargeImage = ConvertToImageSource(byteArray3);
            buttonData2.Image = ConvertToImageSource(byteArray4);


            //6. Create push Button

            //PushButton button1 = panel.AddItem(buttonData1) as PushButton;
            //PushButton button2 = panel.AddItem(buttonData2) as PushButton;

            //7. Add stackable buttons
            //panel.AddStackedItems(buttonData1, buttonData2);

            //8. Add Split Button
            //SplitButtonData splitButtonData = new SplitButtonData("SplitButton", "Split\rButton");
            //SplitButton splitButton = panel.AddItem(splitButtonData) as SplitButton;
            //splitButton.AddPushButton(buttonData1);
            //splitButton.AddPushButton(buttonData2);

            //9. Add pull down button
            panel.AddSeparator();
            panel.AddSlideOut();
            PulldownButtonData pulldownButtonData = new PulldownButtonData("pulldownButton", "Pulldown\rButton");
            pulldownButtonData.LargeImage = ConvertToImageSource(byteArray);

            PulldownButton pulldownButton = panel.AddItem(pulldownButtonData) as PulldownButton;
            pulldownButton.AddPushButton(buttonData1);
            pulldownButton.AddPushButton(buttonData2);

            panel.AddSeparator();
            //10. Other Items

            
           


            return Result.Succeeded;
        }

        private RibbonPanel CreateGetPanel(UIControlledApplication app, string tabName, string panelName1)
        {
            //look for panel in tab
            foreach(RibbonPanel panel in app.GetRibbonPanels(tabName))
            {
                if(panel.Name == panelName1)
                {
                    return panel;
                }
            }

            ////Panel not found, Create it
            //RibbonPanel returnPanel = app.CreateRibbonPanel(tabName, panelName1);
            //return returnPanel;

            return app.CreateRibbonPanel(tabName, panelName1);

        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }

        public BitmapImage ConvertToImageSource(byte[] imageData)
        {
            using (MemoryStream mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                BitmapImage bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.StreamSource = mem;
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.EndInit();

                return bmi;
            }
        }

        public byte[] ConvertBitmapToByteArray(Bitmap bitmap)
        {
            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png); // You can change ImageFormat if needed.
                return memoryStream.ToArray();
            }
        }


    }

}
