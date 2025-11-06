using System;
using System.Collections.Generic;

namespace dmx_dimmer
{
    public class GraphicStageViewState
    {
        public List<FixtureState> Fixtures { get; set; } = new();
        public string? Title { get; set; } = "Stage View";
    }

    public class FixtureState
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string Name { get; set; } = "Fixture";
        public int X { get; set; } = 50;
        public int Y { get; set; } = 50;
        public int Width { get; set; } = 40;
        public int Height { get; set; } = 40;
    }
}
