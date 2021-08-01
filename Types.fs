module Types

type Vec3 (x:float, y:float, z:float) =
    let lengthSquared = x * x + y * y + z * z
    new () = Vec3(0.0, 0.0, 0.0)
    member this.X = x
    member this.Y = y
    member this.Z = z
    member this.Add(other:Vec3) =
        Vec3(this.X + other.X, this.Y + other.Y, this.Z + other.Z)
    member this.Sub(other:Vec3) =
        Vec3(this.X - other.X, this.Y - other.Y, this.Z - other.Z)
    member this.Mul(f:float) =
        Vec3(this.X * f, this.Y * f, this.Z * f)
    member this.Mul2(other:Vec3) =
        Vec3(this.X * other.X, this.Y * other.Y, this.Z * other.Z)
    member this.Div(f:float) =
        //Vec3(this.X * (1.0 / f), this.Y * (1.0 / f), this.Z * (1.0 / f))
        Vec3(this.X / f, this.Y / f, this.Z  / f)
    member this.Dot(other:Vec3) =
        this.X * other.X + this.Y * other.Y + this.Z * other.Z
    member this.Cross(other:Vec3) =
        Vec3(this.Y * other.Z - this.Z * other.Y,
             this.Z * other.X - this.X * other.Z,
             this.X * other.Y - this.Y * other.X)
    member this.LengthSquared() =
        lengthSquared
    member this.Length() =
        sqrt lengthSquared
    member this.UnitVector() =
        this.Div(this.Length())
    member this.WriteColor() =
        $"{int this.X} {int this.Y} {int this.Z}\n"

type Point3 = Vec3

type Color = Vec3

type Ray (origin:Point3, direction:Vec3) =
    member this.Origin = origin
    member this.Direction = direction
    member this.At(t:float) =
        this.Origin.Add(this.Direction.Mul(t))
