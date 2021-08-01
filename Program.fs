open System.IO
open Types

let saveTextFile fileName text =
    File.WriteAllText(fileName, text)

let appendText fileName text =
    File.AppendAllText(fileName, text)

let rayColor (r:Ray) (x:float) : Color =
    let unitDirection = r.Direction.UnitVector()
    let t = 0.5 * unitDirection.Y + 1.0
    let c1 = Color(1.0, 1.0, 1.0).Mul(1.0 - t)
    let c2 = Color(0.5, 0.7, 1.0).Mul(t)
    let c3 = c1.Add(c2)
    let c3X = c3.Mul(x)
    c3X

let gradient i j x =
    // Image
    let aspectRatio = 16.0 / 9.0
    let imageWidth = 400
    let imageHeight = int (float imageWidth / aspectRatio)
    // Camera
    let viewportHeight = 2.0
    let viewportWidth = aspectRatio * viewportHeight
    let focalLenght = 1.0

    let origin = Point3()
    let horizontal = Vec3(viewportWidth, 0.0, 0.0)
    let vertical = Vec3(0.0, viewportHeight, 0.0)
    let lowerLeftCorner = origin.Sub(horizontal.Div(2.0))
                                .Sub(vertical.Div(2.0))
                                .Sub(Vec3(0.0, 0.0, focalLenght))
    
    let u = float j / float (imageWidth - 1)
    let v = float (imageHeight - i) / float (imageHeight - 1)
    let ray = Ray(origin, lowerLeftCorner.Add(horizontal.Mul(u)
                                         .Add(vertical.Mul(v)
                                         .Sub(origin))))
    let rc = rayColor ray x
    rc.WriteColor()

(*
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
*)

let secondImage =
    saveTextFile "out2.ppm" "P3\n400 225\n255\n"
    let arr =
        Array2D.create 400 225 255.999
        |> Array2D.mapi gradient
        |> Array2D.iter (fun x -> appendText "out2.ppm" x)
    arr

[<EntryPoint>]
let main argv =
    //firstImage
    secondImage
    //let v1 = Vec3(1.0, 1.0, 0.0)
    //let v2 = v1.Cross(Vec3(1.0, 0.0, 1.0))
    //let v3 = v2.UnitVector()
    //printfn "%f %f %f" v3.X v3.Y v3.Z
    0 
