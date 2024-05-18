using System;

class VectorRenderer : IRenderer
{
    public void Render(string shape)
    {
        Console.WriteLine($"Drawing {shape} as lines");
    }
}

class RasterRenderer : IRenderer
{
    public void Render(string shape)
    {
        Console.WriteLine($"Drawing {shape} as pixels");
    }
}
