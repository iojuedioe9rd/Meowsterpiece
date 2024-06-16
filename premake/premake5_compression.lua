project "Meowsterpiece.Compression"
    kind "SharedLib"
    language "C#"
    location "src/Meowsterpiece/Compression"
    targetdir (target_dir)
    objdir (obj_dir)

    files {
        "src/Meowsterpiece/Compression/*.cs" -- Include all C# source files in Compression module
    }

    filter "configurations:Debug"
        defines { "DEBUG" }
        symbols "On"

    filter "configurations:Release"
        defines { "NDEBUG" }
        optimize "On"