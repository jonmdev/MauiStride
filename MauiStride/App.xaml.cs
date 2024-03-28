using Stride.CommunityToolkit.Engine;
using Stride.CommunityToolkit.Rendering.ProceduralModels;
using Stride.Core.Mathematics;
using Stride.Engine;
using System.Diagnostics;

namespace MauiStride {
    public partial class App : Application {
        public TestPage testPage;
        public MyGame game;
        public App() {

            //=====================
            //DEFAULT CODE
            //=====================
            //InitializeComponent();
            //MainPage = new AppShell();

            //=====================
            //CUSTOM CODE
            //=====================
            testPage = new();
            this.MainPage = testPage;

            AbsoluteLayout absDummy = new(); //need a dummy absolute layout as root of main page as it doesn't behave properly in all OS's in this position
            testPage.Content = absDummy;

            AbsoluteLayout absRoot = new(); //use this as the actual root element - will behave properly in all OS's.
            absDummy.Add(absRoot);

            Label testLabel = new();
            testLabel.TextColor = Colors.Black;
            testLabel.FontSize = 30;
            testLabel.Text = "HELLO";
            absRoot.Add(testLabel);

            //STRIDE TEST
            Task.Run(delegate {
                game = new();
                game.Run(start: StartGame);

            });
            
            //Debug.WriteLine("FINISHED MAUI APP CONFIG"); //this causes unhandled exception
            
        }
        void StartGame(Scene rootScene) {

            Debug.WriteLine("START GAME");
            game.SetupBase3DScene();

            var entity = game.Create3DPrimitive(PrimitiveModelType.Capsule);

            MySyncScript updateScript = new();
            entity.Add(updateScript);
            entity.Transform.Position = new Vector3(0, 8, 0);

            entity.Scene = rootScene;
        }

    }
    public class MyGame : Game {
        DateTime initDateTime = DateTime.Now;
        protected override void BeginRun() {
            base.BeginRun();
        }
        protected override void Update(Stride.Games.GameTime gameTime) {
            base.Update(gameTime);
            Debug.WriteLine("RUNNING FOR " + (DateTime.Now - initDateTime).TotalSeconds);
        }
    }
    class MySyncScript : SyncScript {
        //Entity entity;
        //public void setEntity(Entity entity) {
        //    this.entity = entity;
        //}
        public override void Update() {
            Debug.WriteLine("SYNC SCRIPT RUN " + Entity.Transform.Position);

        }
    }
    public class TestPage : ContentPage {
        public TestPage() {
            this.BackgroundColor = Colors.AliceBlue;
        }
    }
}
