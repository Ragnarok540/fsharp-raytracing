open System.IO

let saveTextFile fileName text =
    File.WriteAllText(fileName, text)

let appendText fileName text =
    File.AppendAllText(fileName, text)

let generatePixel i j x =
    let r = (float i / 255.0) * x
    let g = (float (255 - j) / 255.0) * x
    let b = 0.25 * x
    let a = [int r; int g; int b]
    $"{a.[0]} {a.[1]} {a.[2]}\n"

[<EntryPoint>]
let main argv =
    saveTextFile "out.ppm" "P3\n256 256\n255\n"
    let arr =
        Array2D.create 256 256 255.999
        |> Array2D.mapi generatePixel

    for i in 0 .. 255 do
        printfn $"Scanlines remaining {256 - i}"
        for j in 0 .. 255 do
            appendText "out.ppm" arr.[j, i]
    0 
