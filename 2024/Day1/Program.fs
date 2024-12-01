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
    |> Array.toList
    |> List.map parseLine
    |> List.unzip
    |> fun (x, y) -> (List.sort x, List.sort y)

let totalDistance =
    List.zip list1 list2
    |> List.map (fun (x, y) -> abs (x - y))
    |> List.sum

printfn $"Total distance: %d{totalDistance}"

let frequencyMap =
    list2
    |> List.groupBy id
    |> List.fold (fun (acc: Dictionary<int, int>) (key, group) ->
        acc.[key] <- group.Length
        acc) (Dictionary<int, int>())
        
let similarityScore =
    list1
    |> List.map (fun item -> (item, frequencyMap.GetValueOrDefault(item) ))
    |> List.map (fun (x, y) -> x * y)
    |> List.sum
    
printfn $"Similarity score: %A{similarityScore}"