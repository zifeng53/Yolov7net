
using IVilson.AI.Yolov7net;
using System.Diagnostics;
using System.Drawing;


namespace Yolov7net.test
{
    public class Yolov7Test
    {
        [Fact]
        public void TestYolov7()
        {
            
            using var yolo = new Yolov7("./assets/best.onnx",true); //yolov7 模型,不需要 nms 操作
            // setup labels of onnx model 
            yolo.SetupYoloDefaultLabels();   // use custom trained model should use your labels like: yolo.SetupLabels(string[] labels)
            Assert.NotNull(yolo);
            using var image = Image.FromFile("Assets/demo.jpg");

            var ret = yolo.Predict(image);
            Assert.NotNull(ret);
            Assert.True(ret.Count == 1);
        }

        [Fact]
        public void TestYolov5()
        {
            using var yolo = new Yolov5("./assets/yolov7-tiny_640x640.onnx"); //yolov5 or yolov7 模型, 需要 nms 操作
            // setup labels of onnx model 
            yolo.SetupYoloDefaultLabels();   // use custom trained model should use your labels like: yolo.SetupLabels(string[] labels)
            Assert.NotNull(yolo);
            using var image = Image.FromFile("Assets/demo.jpg");
            var ret = yolo.Predict(image);
            Assert.NotNull(ret);
            Assert.True(ret.Count == 1);
        }

        [Fact]
        public void TestYolov8()
        {
            using var yolo = new Yolov8("./assets/yolov8n.onnx"); //yolov7 模型,不需要 nms 操作
            // setup labels of onnx model 
            yolo.SetupYoloDefaultLabels();   // use custom trained model should use your labels like: yolo.SetupLabels(string[] labels)
            Assert.NotNull(yolo);
            using var image = Image.FromFile("Assets/demo.jpg");
            var ret = yolo.Predict(image,useNumpy:true);
            Assert.NotNull(ret);
            Assert.True(ret.Count == 1);
        }

        [Fact]
        public void TestPerformance()
        {
            using var yolo = new Yolov7("./assets/yolov7-tiny.onnx");
            yolo.SetupYoloDefaultLabels();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for(int i = 0; i < 10; i++)
            {
                using var image = Image.FromFile("Assets/demo.jpg");
                var ret = yolo.Predict(image);
            }
            sw.Stop();
            Debug.WriteLine(sw.ElapsedMilliseconds);
        }
    }
}
