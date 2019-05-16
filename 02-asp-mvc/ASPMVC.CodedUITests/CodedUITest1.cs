using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace ASPMVC.CodedUITests
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class CodedUITest1
    {
        public CodedUITest1()
        {
        }

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            Playback.Initialize();
            var bw = BrowserWindow.Launch(new Uri("http://localhost:5000"));
            bw.CloseOnPlaybackCleanup = false;
        }

        [TestInitialize]
        public void SetUp()
        {
            //this.UIMap.InitIndexPage();
            this.UIMap.LogInAsAdmin();
        }

        [TestMethod]
        public void CodedUITestCreate()
        {
            this.UIMap.AssertAdminIsLogged();
            this.UIMap.GoToGunsSection();
            this.UIMap.GoToCreateNew();
            this.UIMap.CreateNewGun();
            this.UIMap.CheckAddedGunDetails();
            this.UIMap.AssertAddedUSP();
            this.UIMap.GoBackToGuns();
            this.UIMap.EditAddedGun();
            this.UIMap.CheckAddedGunDetails();
            this.UIMap.AssertEditedUSP();
            this.UIMap.GoBackToGuns();
            this.UIMap.DeleteAddedGun();
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        }

        [TestMethod]
        public void CodedUITestEdit()
        {
            this.UIMap.AssertAdminIsLogged();
            this.UIMap.GoToGunsSection();
            this.UIMap.AssertExistingMP5();
            this.UIMap.AssertExistingMP5Price();
            this.UIMap.EditExistingGun();
            this.UIMap.AssertEditedMP5();
            this.UIMap.AssertEditedMP5Price();
        }

        [TestCleanup]
        public void CleanUp()
        {
            this.UIMap.GoBackToIndexAndLogout();
        }

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if (this.map == null)
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
