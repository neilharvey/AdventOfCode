open System
open System.Collections.Generic
open System.IO

let args = Environment.GetCommandLineArgs()

let parseLine (line: string) =
    let parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
    let x = int parts.[0]
    let y = int parts.[1]
    x, y

let list1, list2 =
    File.ReadAllLines(args.[1])
    |> Array.map parseLine
    |> Array.unzip
    |> fun (x, y) -> (Array.sort x, Array.sort y)

let totalDistance =
    Array.zip list1 list2
    |> Array.map (fun (x, y) -> abs (x - y))
    |> Array.sum

printfn $"Total distance: %d{totalDistance}"

let frequencyMap =
    list2
    |> Array.groupBy id
    |> Array.fold (fun (acc: Dictionary<int, int>) (key, group) ->
        acc.[key] <- group.Length
        acc) (Dictionary<int, int>())
        
let similarityScore =
    list1
    |> Array.map (fun item -> (item, frequencyMap.GetValueOrDefault(item) ))
    |> Array.map (fun (x, y) -> x * y)
    |> Array.sum
    
printfn $"Similarity score: %A{similarityScore}"