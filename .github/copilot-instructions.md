# GitHub Copilot Instructions for RimWorld Modding Project

## Mod Overview and Purpose

This RimWorld mod aims to enhance the game’s fire dynamics and prevention systems by introducing new mechanics related to flammable materials and fire management. It provides a more immersive and strategic approach to handling fire hazards by implementing interconnected systems of flammable materials, contextual fire ignition scenarios, and expanded firefighting options.

## Key Features and Systems

1. **Sprinkled Fuel System**: 
    - Enables the creation of structures or areas that can more easily catch fire when triggered by specific activities or events.
    - Implemented via the `Building_SprinkledFuel` class which monitors potential ignition sources such as pawns and projectiles.

2. **Flammable Links**:
    - Allows fire spread through linked components, increasing the strategic importance of managing fuel-prone environments.
    - Utilizes `CompProperties_FlammableLink` and `FlammableLinkComp` to establish connections and simulate heat and fire spreading mechanics.

3. **Designator Tools**:
    - Introduces specific designators like `Designator_AreaNoFireFighting` to define areas where firefighting is discouraged.
    - Provides tools for area expansion and restriction management.

4. **Graphic Enhancements**:
    - Enhances visual representation of linked structures using the `Graphic_LinkedCornerComplement` class for more dynamic visuals.

5. **Fire Ignition Definitions**:
    - Customizable ignition conditions via the `IgnitionDef` to define unique scenarios and responses based on environmental and object states.

6. **WorkGiver Extensions**:
    - Extends firefighting job capabilities with `WorkGiver_ExtendedFightFires` for nuanced fire management tasks within colonist AI routines.

## Coding Patterns and Conventions

- Classes are organized based on functionality, with clear separation between internal mechanics and public interfaces.
- Internal classes are used to encapsulate and protect logic, minimizing exposure and potential interference with the existing game code.
- Method names use camelCase for private methods and PascalCase for public methods.

## XML Integration

- XML is used for defining mod assets, settings, and mappings that are consumed by the game engine and the mod's C# logic.
- Configuration and content definitions (e.g., flammable properties, designator layers) should be correctly linked to C# classes and methods that process these definitions.

## Harmony Patching

- Harmony is used to insert new logic or modify existing game methods without altering the core game files. Ensure that patches are well-documented and reversible.
- Identify areas for potential Harmony patches by exploring classes and methods that influence fire dynamics, such as pawn behaviors or environmental interactions.

## Suggestions for Copilot

When using GitHub Copilot to assist in code completion and generation in this modding project, consider the following suggestions:

1. **Consistent Commenting**: Continuously add comments to describe method functionalities and expected behaviors, making it easier for Copilot to generate contextually aware suggestions.

2. **Parameter Descriptions**: Define parameters clearly in method signatures to help Copilot suggest appropriate logic and usage when calling these methods.

3. **Method Templates**: Create method templates for common tasks (e.g., connecting flammable links, triggering fire events, drawing graphics) to encourage consistent and efficient code generation.

4. **Use Existing Patterns**: Leverage existing coding patterns by providing detailed examples in the codebase for common tasks like Harmony patching and XML parsing.

5. **Ask for Help**: Prompt Copilot for specific tasks, such as “Generate a Harmony patch to modify `Pawn_TryFireWatch` behavior” to gain targeted suggestions that match the mod's purpose.

By integrating these considerations into your development routine, you can effectively utilize GitHub Copilot to streamline the coding process while aligning with the modding project’s objectives and coding standards.
