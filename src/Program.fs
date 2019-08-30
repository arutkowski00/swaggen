// Learn more about F# at http://fsharp.org

open System
open Argu
open Specification

type Arguments =
    | [<AltCommandLine("-s"); Mandatory>] Src of urlOrPath:string
    | [<AltCommandLine("-o"); Mandatory>] Out_Dir of path:string
    | [<AltCommandLine("-b")>] Base_Url of url:string
    | [<AltCommandLine("-v")>] Verbose
    | Version
with
    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | Src _ -> "url or path to the OpenAPI specification"
            | Out_Dir _ -> "path to the output directory"
            | Base_Url _ -> "default URL for clients"
            | Verbose _ -> "print a lot of output to stdout"
            | Version _ -> "print version"

type LogMessage =
    | Debug of string
    | Info of string
    | Warn of string
    | Error of string

let logger logMessage = 
    match logMessage with
    | Debug msg -> printfn "DEBUG %s" msg
    | Info msg -> printfn "INFO %s" msg
    | Warn msg -> printfn "WARN %s" msg
    | Error msg -> printfn "ERROR %s" msg

    
let run options =
    Specification.getSpecification
    ()

[<EntryPoint>]
let main argv =
    let parser = ArgumentParser.Create<Arguments>(programName = "swaggen.exe")

    try
        let parseResults = parser.ParseCommandLine(inputs = argv)
        let options = parseResults.GetAllResults()
        run options
    with e ->
        printfn "%s" e.Message
    0 // return an integer exit code
