open System.IO

let saveTextFile fileName text =
    File.WriteAllText(fileName, text)

let appendText fileName text =
    File.AppendAllText(fileName, text)

let generatePixel i j x =
    let r = (float i / 255.0) * x
    let g = (float (255 - j) / 255.0) * x
    let b = 0.25 * x
    let a =[int r; int g; int b]
    $"{a.[0]} {a.[1]} {a.[2]}\n"

let createArray length1 length2 =
    let a2d = Array2D.create length1 length2 255.999
    a2d

[<EntryPoint>]
let main argv =
    saveTextFile "out.ppm" "P3\n256 256\n255\n"
    let arr = createArray 256 256
    let arr2 =
        arr
        |> Array2D.mapi generatePixel

    for i in 0 .. 255 do
        printfn $"Scanlines remaining {256 - i}"
        for j in 0 .. 255 do
            appendText "out.ppm" arr2.[j, i]
    0 
