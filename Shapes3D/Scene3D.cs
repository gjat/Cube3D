namespace Shapes3D;

public class Scene3D 
{
    public List<Line3D> lines = [];

    public void Add(Line3D line)
    {
        lines.Add(line);
    }

    public Scene3D TranslateScene(int offsetX, int offsetY, int offsetZ)
    {
        var newScene = new Scene3D();        
        foreach(var line in lines)
        {
            newScene.Add(line.Offset(offsetX, offsetY, offsetZ));
        }
        return newScene;
    }

    public Scene3D RotateX(double angle)
    {
        var sinAngle = Math.Sin(angle);
        var cosAngle = Math.Cos(angle);

        var newScene = new Scene3D();
        foreach(var line in lines)
        {
            newScene.Add(line.RotateX(sinAngle, cosAngle));
        }
        return newScene;
    }

    public Scene3D RotateY(double angle)
    {
        var sinAngle = Math.Sin(angle);
        var cosAngle = Math.Cos(angle);

        var newScene = new Scene3D();
        foreach(var line in lines)
        {
            newScene.Add(line.RotateY(sinAngle, cosAngle));
        }
        return newScene;
    }

}
