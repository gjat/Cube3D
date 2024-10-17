namespace Shapes3D;
public struct Point3D 
{
    public int X;
    public int Y;
    public int Z;

    public Point3D Offset(int offsetX, int offsetY, int offsetZ)
    {
        return new Point3D() { X = X + offsetX, Y = Y + offsetY, Z = Z + offsetZ };
    }

    public Point3D RotateX(double sinAngle, double cosAngle)
    {
      return new Point3D() { 
        X = X, 
        Y = (int)((Y * cosAngle) - (Z * sinAngle)), 
        Z = (int)((Z * cosAngle) + (Y * sinAngle)) };
    }

    public Point3D RotateY(double sinAngle, double cosAngle)
    {
      return new Point3D() { 
        X = (int)((X * cosAngle) + (Z * sinAngle)),
        Y = Y, 
        Z = (int)((Z * cosAngle) - (X * sinAngle)) };
    }
}
