# Compiling
You will require `dotnet` on your machine; in particular, you will need `6.0`.

Run `./publish.sh {platform}` (e.g. `win`, `osx`, `linux`) to get a compiled version of the game. This will create an executable in `/Turtle/bin/debug/net6.0/{platform}/publish/`.

This binary is not cross-platform, but is self-contained and can be shared.

# Getting Started
*Not* a clone of any particular game that was recently bought by NYT, but with extensible generators...

Turtle can be started by running `./Turtle` in whichever directory the binary is located in. You'll be good to go, except for configuration of the game itself.

# Configuration
Turtle uses environment variables for configuration, as most pertain to display or handling connections.
```
ANSI_PREFIX = "\x1b[" // Describes what characters to use. String.

BLOCK_WIDTH = 5 // Width of each grid block. Integer.
BLOCK_HEIGHT = 3 // Height of each grid block. Integer.

SHOW_TITLE = true // Shows or hides the title. Boolean.
SHOW_KEYBOARD = true // Shows or hides the keyboard. Boolean.
HARD_MODE = false // Enables or disables hardmode. Boolean.

BAD_CHAR_BACK = rgb6bit(5,3,3) // The background colour for bad characters. Color.
BAD_CHAR_FORE = legacy(Black) // The foreground colour for bad characters. Color.
BAD_POS_BACK = rgb6bit(5,4,2) // The background colour for bad positions. Color.
BAD_POS_FORE = legacy(Black) // The foreground colour for bad positions. Color.
GOOD_CHAR_BACK = rgb6bit(3,4,1) // The background colour for good characters. Color.
GOOD_CHAR_FORE = legacy(Black) // The foreground colour for good characters. Color.
DEFAULT_BACK = gray(4) // The background colour for empty blocks. Color.
DEFAULT_FORE = legacy(White) // The foreground colour fore empty blocks. Color.

REPO = "https://raw.githubusercontent.com/AllyMarthaJ/OpenTurtleGenerators/master/generators.json" // The repo to fetch named games from. String.
STORE_GENERATOR = true // Whether or not to save named fetched games. Boolean.

GAME = "example" // The name of the generator to use/game to play. String.
SEED = 0 // The seed to use. 0 for today's. Integer.
```

## Colours
There is a set of colour syntaxes used to support a variety of terminals:
- `true(r,g,b)` is true colour and supports proper RGB channels (0-255).
- `rgb6bit(r,g,b)` is basic colour support and supports a lower resolution colour set (0-5 per channel).
- `gray(s)` is grayscale and supports a set of grays (0-23).
- `legacy(name)` supports legacy terminal colours (e.g. `Red`/`BrightRed`).

## Example
For example, you could run a minimal `common` game with:
```
SHOW_TITLE=false SHOW_KEYBOARD=false GAME=common ./turtle
```

# Contributing to Generators!
The great thing about this is you can contribute your own games! Take a look at https://github.com/AllyMarthaJ/OpenTurtleGenerators for inspiration for code, but the important thing is you implement the `IGenerator` interface. This is exposed in the `Core` project - so you can create your project or temporarily add to that!

After you're done, open a branch on the repository above amending both the main repository JSON file and adding your own folder & script. Open a PR and I'll approve it if it's safe code :)
