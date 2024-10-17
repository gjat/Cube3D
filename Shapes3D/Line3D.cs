namespace Shapes3D;

public struct Line3D 
{
    public Point3D A;
    public Point3D B;

    public Line3D(int X1, int Y1, int Z1, int X2, int Y2, int Z2)
    {
        A = new Point3D() { X = X1, Y = Y1, Z = Z1};
        B = new Point3D() { X = X2, Y = Y2, Z = Z2};
    }

    public Line3D(Point3D a, Point3D b)
    {
        A = a; B = b;
    }

    public Line3D Offset(int offsetX, int offsetY, int offsetZ)
    {
        return new Line3D(A.Offset(offsetX, offsetY, offsetZ), 
            B.Offset(offsetX, offsetY, offsetZ));
    }

    public Line3D RotateX(double sinAngle, double cosAngle)
    {
        return new Line3D(A.RotateX(sinAngle, cosAngle),
            B.RotateX(sinAngle, cosAngle));
    }

    public Line3D RotateY(double sinAngle, double cosAngle)
    {
        return new Line3D(A.RotateY(sinAngle, cosAngle),
            B.RotateY(sinAngle, cosAngle));
    }
}
