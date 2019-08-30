module Specification

open System

type SourceKind =
    | Http of url:string
    | File of path:string

let isUrl s = 
    let isHttpOrHttps scheme = scheme = Uri.UriSchemeHttp || scheme = Uri.UriSchemeHttps

    match Uri.TryCreate(s, UriKind.Absolute) with
    | true, uriResult -> isHttpOrHttps uriResult.Scheme
    | _ -> false

let isPathToFile s = 
    let isFile scheme = scheme = Uri.UriSchemeFile

    match Uri.TryCreate(s, UriKind.Absolute) with
    | true, uriResult -> isFile uriResult.Scheme
    | _ -> System.IO.File.Exists(s)
    
let getSourceKind s = match s with
    | url when isUrl url -> Some(SourceKind.Http url)
    | path when isPathToFile path -> Some(SourceKind.File path)
    | _ -> None

let getSpecification = 
    0
