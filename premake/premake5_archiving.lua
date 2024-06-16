project "Meowsterpiece.Archiving"
    kind "SharedLib"
    language "C#"
    location "src/Meowsterpiece/Archiving"
    targetdir (target_dir)
    objdir (obj_dir)

    files {
        "src/Meowsterpiece/Archiving/*.cs" -- Include all C# source files in Archiving module
    }

    filter "configurations:Debug"
        defines { "DEBUG" }
        symbols "On"

    filter "configurations:Release"
        defines { "NDEBUG" }
        optimize "On"