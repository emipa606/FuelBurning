# GitHub Copilot Instructions for FuelBurning Mod

## Mod Overview and Purpose

The FuelBurning mod for RimWorld is an update to Steardliy's original mod. It adds new types of fuel burning traps to the security category, which ignite upon impact. The traps can be ignited by direct attacks or nearby gunfire, accumulating heat until they reach a threshold and ignite. Players must be cautious when dealing with these traps, as they can potentially burn their colony. Additionally, this mod includes a "no firefighting" zone tool that may not be immediately apparent from the description.

## Key Features and Systems

1. **Fuel Burning Traps**: Three types of fuel burning traps are added to the security category. These traps ignite when struck or when heat from nearby gunfire accumulates to a certain level.

2. **No Firefighting Zone Tool**: A tool that allows players to designate areas where firefighting is not allowed, adding another layer of strategic gameplay.

3. **Heat Accumulation System**: Heat accumulates in the fuel when attacked or when exposed to gunfire. The fuel ignites when its heat exceeds a certain threshold.

4. **Flammable Link Comp**: This component manages the heat accumulation and ignition logic for the fuel burning traps.

5. **Ignition Def**: A definition for the ignition system, which includes properties such as the heat ratio and ignition chance.

## Coding Patterns and Conventions

- **C# File Structure**: C# files are organized into classes that extend base RimWorld classes. For example, `Building_SprinkledFuel` extends `Building`, and `Designator_CleanFuel` extends `Designator`.

- **Method Naming**: Methods are named descriptively to reflect their functionality. For instance, `checkBulletFire` and `checkSparksFromPawn` in `Building_SprinkledFuel` indicate their purpose.

- **Component System**: Components like `FlammableLinkComp` are used to add functionality to buildings and other entities. They manage state and behavior related to the fuel burning system.

## XML Integration

XML files are used to define various aspects of the mod, including effects, sounds, and stats. Here are some examples:

- **Effecter_Construction.xml**: Defines effecter definitions, mote definitions, and sound definitions.
- **Mote.xml**: Defines mote definitions.
- **IgnitionDef.xml**: Defines ignition definitions, including damage definitions and specific properties for the FuelBurning mod.
- **Interact_Sustainers_Work.xml**: Defines sound definitions for interactive sustainers.
- **Stats_SprinkledFuel.xml**: Defines stat definitions for sprinkled fuel.

## Harmony Patching

Harmony patching is used to modify the behavior of existing RimWorld methods. This is particularly useful for adding new functionality without modifying the original game code. In the FuelBurning mod, harmony patches might be used to:

- Modify the behavior of buildings to include heat accumulation and ignition logic.
- Add new designators for cleaning fuel.
- Adjust the behavior of pawns and projectiles to interact with fuel burning traps.

## Suggestions for Copilot

1. **Provide Context**: When using Copilot, always provide context by specifying the purpose of the code snippet you need. For example, if you need a method to handle heat accumulation in a fuel burning trap, clearly state that.

2. **Use Descriptive Method Names**: When generating methods, use descriptive names that clearly indicate their purpose. This will help Copilot generate more accurate and relevant code.

3. **Leverage Existing Patterns**: Copilot can be very effective when you leverage existing coding patterns and conventions. For example, if you need a new component, refer to existing components like `FlammableLinkComp` for guidance.

4. **XML Definitions**: When working with XML definitions, provide the specific XML file and the type of definition you need. For instance, if you need a new `ThingDef`, specify the context and any relevant properties.

5. **Harmony Patches**: When generating harmony patches, provide the original method you are patching and describe the changes you want to make. This will help Copilot generate the correct patch code.

## Example Usage Scenarios

1. **Adding a New Fuel Trap**: If you need to add a new type of fuel burning trap, use Copilot to generate a new class that extends an existing building class. Include methods for handling heat accumulation and ignition.

2. **Designating No Firefighting Zones**: If you need to add the "no firefighting" zone tool, use Copilot to generate the necessary designator class and methods for handling zone designation.

3. **Modifying Heat Accumulation Logic**: If you need to modify the heat accumulation logic in an existing component, use Copilot to generate the necessary method or property changes.

4. **Creating a Harmony Patch**: If you need to create a harmony patch to modify the behavior of an existing method, use Copilot to generate the patch code, including the necessary attributes and logic.

By following these guidelines, you can effectively use GitHub Copilot to enhance your FuelBurning mod development process.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

