# FemaleBodyVariants

This mod provides a way to load, on the fly, a different body texture for thin, fat and hulk females.
It will use these names:

Naked_Thin_Female_south.png
Naked_Thin_Female_east.png
Naked_Thin_Female_north.png

Naked_Fat_Female_east.png
Naked_Fat_Female_north.png
Naked_Fat_Female_south.png

Naked_Hulk_Female_east.png
Naked_Hulk_Female_north.png
Naked_Hulk_Female_south.png

It comes with simple female versions of these bodies, so if you don't use body retextures then just using this mod is enough to have the new body types.
If a texture mod has the files above, they are used instead.
It also works for fur covered bodies, like FurCovered_Thin_Female_south.png, and body tattoos follow the female body shape.

Races with their own body textures are left alone, it only changes pawns using the normal vanilla body.

Anyone is free to use this mod as a pre requisite for your own mod.

My female body textures that use this mod, based on Beautiful Bodies from ireth92:
https://steamcommunity.com/sharedfiles/filedetails/?id=3215805750

Steam version: https://steamcommunity.com/sharedfiles/filedetails/?id=3207125987

Requires Harmony.

## Versions

1.7 and 1.6 are built from the code in the Source folder.

1.4 and 1.5 use the old dll I released for them (Legacy folder).

## Building

You need the .NET 10 SDK, then inside this folder run:

    dotnet build Source -c Release
    dotnet build Source -c Release -p:RimWorldVersion=1.6

The first one builds 1.7/Assemblies and the second one 1.6/Assemblies. To test it, copy or link this folder to RimWorld/Mods.

## License

CC0, you can do whatever you want with it, no need to give credits.

AI was used to help code/fix issues
