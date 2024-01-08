using TrionesNET;

namespace TrionesNet_Tests
{
    [TestClass]
    public class TrionesNET_Tests
    {
        [TestMethod]
        public async Task Connect_ShoudBe_Ok()
        {
            var light = new TrionesNET.TrionesNET("Triones-A115220018C9");
            await light.Connect();                     
        }

        [TestMethod]
        public async Task TurnOn_ShoudBe_Ok()
        {
            var light = new TrionesNET.TrionesNET("Triones-A115220018C9");
            var rst = await light.Connect();

            await light.TurnOn();
        }

        [TestMethod]
        public async Task TurnOff_ShoudBe_Ok()
        {
            var light = new TrionesNET.TrionesNET("Triones-A115220018C9");
            var rst = await light.Connect();

            await light.TurnOff();
        }

        [TestMethod]
        public async Task SetColor_ShoudBe_Ok()
        {
            var light = new TrionesNET.TrionesNET("Triones-A115220018C9");
            var rst = await light.Connect();
                      
            var color = new TrionesNET.Color(0x00, 0xFF, 0x00);
            await light.SetColor(color);                      
        }

        [TestMethod]
        public async Task SetWhite_ShoudBe_Ok()
        {
            var light = new TrionesNET.TrionesNET("Triones-A115220018C9");
            var rst = await light.Connect();

            byte brightness = 0x50;
            await light.SetWhite(brightness);           
        }

        [TestMethod]
        public async Task SetBuiltInMode_ShoudBe_Ok()
        {
            var light = new TrionesNET.TrionesNET("Triones-A115220018C9");
            var rst = await light.Connect();
  
            BuiltInMode mode = BuiltInMode.Red_gradual_change;
            byte speed = 0x01;
            await light.SetPresetPatternAsync(mode, speed);
        }
    }
}