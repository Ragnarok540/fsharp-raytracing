open System.IO

type Vec3 (x:float, y:float, z:float) =
    new () = Vec3(0.0, 0.0, 0.0)
    member this.X = x
    member this.Y = y
    member this.Z = z
    member this.Sum(other:Vec3) =
        Vec3(this.X + other.X, this.Y + other.Y, this.Z + other.Z)
    member this.Product(f:float) =
        Vec3(this.X * f, this.Y * f, this.Z * f)

let saveTextFile fileName text =
    File.WriteAllText(fileName, text)

let appendText fileName text =
    File.AppendAllText(fileName, text)

let generatePixel i j x =
    let r = (float j / 255.0) * x
    let g = (float (255 - i) / 255.0) * x
    let b = 0.25 * x
    let a = [int r; int g; int b]
    $"{a.[0]} {a.[1]} {a.[2]}\n"

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
    let v1 = Vec3(1.0, 2.0, 3.0)
    let v2 = v1.Sum(Vec3(3.0, 2.0, 1.0))
    printfn "%f %f %f" v2.X v2.Y v2.Z
    0 
