open System.IO

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
        Vec3(this.X * (1.0 / f), this.Y * (1.0 / f), this.Z * (1.0 / f))
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

let saveTextFile fileName text =
    File.WriteAllText(fileName, text)

let appendText fileName text =
    File.AppendAllText(fileName, text)

let generatePixel i j x =
    let pixelColor = Color(float j / 255.0, float (255 - i) / 255.0, 0.25)
    let pixelColorX = pixelColor.Mul(x)
    pixelColorX.WriteColor()

let firstImage =
    saveTextFile "out.ppm" "P3\n256 256\n255\n"
    let arr =
        Array2D.create 256 256 255.999
        |> Array2D.mapi generatePixel
        |> Array2D.iter (fun x -> appendText "out.ppm" x)
    arr

[<EntryPoint>]
let main argv =
    //firstImage
    let v1 = Vec3(1.0, 1.0, 0.0)
    let v2 = v1.Cross(Vec3(1.0, 0.0, 1.0))
    let v3 = v2.UnitVector()
    printfn "%f %f %f" v3.X v3.Y v3.Z
    0 
