// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

namespace OwinFactorySwap.Console
module Nancy =
    open Nancy
    type App() as x =
      inherit NancyModule()
      do
        x.Get.["/"] <- fun _ -> "Hello World" :> obj


module OwinStartup =
    open Owin   
    open Nancy.Owin
    type Startup () =
        member __.Configuration(app : IAppBuilder) =
            app.UseNancy() 
            |> ignore

module Main =
    open System
    open Microsoft.Owin.Hosting

    [<EntryPoint>]
    let main argv = 
        try
            let port = 8085
            let options = StartOptions ()
            options.Port <- port |> Nullable<int>
            options.ServerFactory <- "Nowin"
            //options.ServerFactory <- "Suave.Owin"
            use app = WebApp.Start<OwinStartup.Startup>(options)
            printfn "Server started on port %d" port
            Console.ReadLine() |> ignore
        with ex -> printfn "%A" ex
        Console.ReadLine() |> ignore
        printfn "%A" argv
        0 // return an integer exit code

