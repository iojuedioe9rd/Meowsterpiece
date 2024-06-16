-- premake5.lua

-- Include the other Premake scripts


-- Define common settings
workspace "Meowsterpiece"
    configurations { "Debug", "Release" }
    location "src"

    -- Common paths
    local target_dir = "bin/%{cfg.buildcfg}"
    local obj_dir = "obj/%{cfg.buildcfg}"

    include("./premake/premake5_archiving.lua")
include("./premake/premake5_compression.lua")

    -- Main project (DLL)
    project "Meowsterpiece"
        kind "SharedLib"  -- Change to SharedLib for DLL
        language "C#"
        location "src"
        targetdir (target_dir)
        objdir (obj_dir)

        files {
            "src/Meowsterpiece/**/*.cs" -- Include all C# source files in Meowsterpiece project
        }

        links {
            "System.IO.Compression",
			"System"
        }

        filter "configurations:Debug"
            defines { "DEBUG" }
            symbols "On"

        filter "configurations:Release"
            defines { "NDEBUG" }
            optimize "On"

    project "MyApplication"
        kind "ConsoleApp"
        language "C#"
        location "src"
        targetdir (target_dir)
        objdir (obj_dir)

        files {
            "src/MyApplication/**/*.cs"  -- Include your application source files
        }

        links {
            "Meowsterpiece"  -- Link to Meowsterpiece DLL
        }

        filter "configurations:Debug"
            defines { "DEBUG" }
            symbols "On"

        filter "configurations:Release"
            defines { "NDEBUG" }
            optimize "On"