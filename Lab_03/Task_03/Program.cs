class Program
{
    static void Main(string[] args)
    {
        IRenderer vectorRenderer = new VectorRenderer();
        IRenderer rasterRenderer = new RasterRenderer();

        Shape circle = new Circle(vectorRenderer);
        circle.Draw();

        Shape square = new Square(rasterRenderer);
        square.Draw();

        Shape triangle = new Triangle(vectorRenderer);
        triangle.Draw();

        // You can easily switch renderers
        Shape anotherCircle = new Circle(rasterRenderer);
        anotherCircle.Draw();

        // Or use the same renderer for different shapes
        Shape anotherSquare = new Square(vectorRenderer);
        anotherSquare.Draw();
    }
}
