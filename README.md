<h1 align="center">Brownie Blaster</h1>
<p align="center"><strong>Captain Blaster With Dynamic Bullet Spread</strong>
<br>
Needs: 1 script, ~35 lines</p>
<div align="center"><img src="demo.mp4"></div>

## Feature Goal
To successfully implement the feature, the following gameplay criteria was required:
- Captain Blaster can fire multiple bullets at once
- Fired bullets are always evenly spaced, and fly in the direction they are facing
- Any number of bullets can be fired, without throwing off the rotation, alignment, or velocity
- Bullets can be spread as far apart or as close together as desired

## Notable Challenges
Given that the number of bullets can be either even or odd, sometimes there will be a bullet in the middle, and sometimes there won't. My solution uses the integer data type for bullet count to make sure that the middle point is always a whole number, and uses the modulo operator (%) to execute different logic on even numbers and odd numbers. If you are curious how this works, you can research modulo math and determining parity in C#, or ask Jester to explain.

It was also challenging to determine the right offset numbers when they have to start from the middle, and any iterative process will start at the beginning. To solve this, my algorithm calculates the middle point first, and then generates a list of offsets based on how far each bullet should be from that center. This requires a collection datatype (in this case List<>) and for loops. These are both great subjects for research, and Jester or Professor Loop would be happy to explain more if you ask.

It is also difficult to implement a system like this while the projectiles are handling their own movement as that movement won't yet factor in the rotation. To get around this, the PlayerController adjusts both the rotation and the velocity of the projectiles after Instantiate()ing them, and the projectiles are only responsible for cleaning themselves up.

## Key Takeaways
Pay attention to the following sections of code to make sure you understand how the solution works. This will help you apply relevant pieces to similar problems you encounter on your own.
1. You will likely trigger this method differently than in this example, but **line 25** shows how to trigger it with the `SpreadBullets()` method
2. On **line 33**, we not only `Instantiate()` the projectile, we assign it to a variable. This is crucial as it is what allows us to modify the GameObject after spawning.
3. The `SpreadBullets()` method starting on **line 43** contains the algorithm itself. This is the crucial piece to understand if you want to implement a similar algorithm yourself, so be sure to ask Jester for help if you need it. Pay especially close attention to the conditional logic on **lines 52 and 53** as your spread will not be centered without this.

## Inspector Steps
If you want to test this project yourself or implement a similar solution in your own project, pay attention to the following Inspector workflow steps:
- The `bulletSpeed`, `count`, and `spread` variables are not assigned in the script, so you will have to assign them yourself. You can assign them directly via the inspector, or play around with the sliders in the game view to see the changes in fullscreen play mode. The sliders are locked to specific ranges, so use the Inspector to try especially small or large numbers.
- You should not need to assign any of the object references here, but make sure you do so in your own projects to avoid NullReference Exceptions.
- Ignore the Example Debugging GUI elements in the Inspector and in the scripts, unless you want to implement similar functionality in your own projects.

## Review the Code
Just like in your local Unity projects, the code is located at [./Assets/Scripts/](/Assets/Scripts). The basic textbook logic has not been updated to reflect the new Input System implementation, the singleton pattern for GameManager, or any other newer change, but the relevant logic will work along with these upgrades, and the code is heavily commented to guide you to the important parts. All new code is located in the following script files:
- [PlayerController.cs](/Assets/Scripts/PlayerController.cs)

## Build the Project
This repository is built from a valid Unity project. Unity Hub only needs these three directories (Assets, Packages, and Project Settings) to rebuild the project, so you can Add it to hub just like any other project. Follow the steps below:
1. Use the green "<> Code" button in the top right (pictured below) to download the project archive.
2. Use your OS's file system or a third party tool to extract the archive (let Jester or Professor Loop know if you need help with this).
3. Open and sign in to Unity Hub.
4. Navigate to the "Projects" section, and use the Add > Add Project from Disk option to add the project to Unity Hub.
5. Click on the project in your Projects list to open it in Unity Editor and test it.

![Screenshot of Code button](https://docs.github.com/assets/cb-13128/mw-1440/images/help/repository/code-button.webp)

## Contributing
If you are familiar with GitHub and with the open source process, and you see something in this example project which could be improved, feel free to open a pull request. I make no guarantee that I will see or merge it, and students are NOT REQUIRED to contribute. This project exists as a resource for you, not a graded assignment. I will not be providing assistance with contributing to this project if you are not familiar with the process.

## Unrelated Lessons
None of these techniques are required for this feature implementation, but they offer more efficient, more standard, or more convenient ways to accomplish certain tasks. If you want to expand your horizons or implement some more advanced conventions into your Captain Blaster project, this can be a good place to start.

On **line 15**, the following code uses the `[Header]` attribute:
```cs
[Header("Bullet Pattern Parameters")]
```
This attribute just adds the provided string as a header in the Inspector to keep long lists of Inspector properties organized, as seen below:

![alt](HeaderAttributeScreenshot.png)

***

On **line 17**, the following code uses the `[Tooltip]` attribute:
```cs
[SerializeField, Tooltip("How many bullets should be spawned per shot?")] private int count;
```
This attribute just adds the provided string as a tooltip in the Inspector when hovering over this field. Try hovering over "Count" or "Spread" in the Inspector for the Player object to see it in action.

***

**Line 38** uses the `TryGetComponent()` method to make sure that we don't try to execute code on a component that can't be found:
```cs
if (bullet.TryGetComponent(out Rigidbody2D rigidBodyReference)) rigidBodyReference.linearVelocity = bullet.transform.up * bulletSpeed;
```

This line is long and complicated, so I've broken it down below:
```cs
if (bullet.TryGetComponent()) {}
```
`TryGetComponent()` works like `GetComponent<T>()`, but it returns a boolean value, so we can use it to check if the component was successfully found. The rest of the line won't run at all if no matching component was found.

```cs
(out Rigidbody2D rigidBodyReference)
```
Just like usual, we feed a parameter into the method, this time a `RigidBody2D` called `rigidBodyReference`. The `out` keyword means that the method will store a value in that variable itself, so that we can access it later. This is important because we want `TryGetComponent()` to return a bool, but we still need access to the component if it finds one.

```cs
rigidBodyReference.linearVelocity =
```
Since we used this variable as a parameter for `TryGetComponent()` and used the out keyword, we can now modify it knowing for certain that it will never be null or otherwise unusable.

```cs
bullet.transform.up * bulletSpeed;
```
Lastly, the value we use for the `RigidBody2D`'s `linearVelocity` is our defined `bulletSpeed` in the direction the bullet is facing (`transform.up`). This is a more advanced method, and it is not much better than what you've already seen, but it is something to be aware of and mess around with if you are ready.
***

Because we need to store a lot of offset values, and we won't know how many until the method is called, we initialize a `List<T>` on **line 47**:
```cs
List<float> offsets = new();
```
Notice the `Generic<T>` notation like we see with `GetComponent<T>()`. This syntax allows us to pass any datatype in as `T` so that we don't need a different method or object for every possible datatype. In this case, it lets us use type, `List<T>` from the `System.Collections.Generic` namespace, for any type of data we might want a list of. In this example, we are storing floating-point values, so we initialize a list of floats as `List<float>`. Since we specify the type right off the bat, the `new()` keyword already knows to make a new `List<float>`.

We also call the `Add()` method of the `List<T>` type on **line 54**:
```cs
offsets.Add(offset * spread);
```
There are many ways to manage the contents of lists in C#, but this one is the most self-explanatory. It simply adds the provided item to the list. This example never needs to directly access specific items in the list, but if you need to do so, you can use the indexing syntax shown below:
```cs
List<string> strings = new();

strings.Add("Hello");
strings.Add("World");

Debug.Log(strings[0]) // logs: Hello
Debug.Log(strings[1]) // logs: World
```
Remember that the number inside the [square brackets] is the INDEX, not the item's ranking in the list, so it starts at ZERO, not at one.

***

The following for loop can be found on **line 49**:
```cs
for (int i = 1; i <= count; i++) {}
```

For loops are syntactically daunting in C#, but they are incredibly powerful once you understand them. They allow us to repeatedly execute code with slightly different information or on different objects. Each for loop has 3 components, separated by semicolons:
1. The "initializer" which creates a variable the loop will use to track its state
2. The "condition" which the loop uses to determine when it should stop iterating
3. The "iterator" which is executed once on every iteration and is usually used to increment the initializers variable

```cs
for ([1]int i = 1; [2] i <= count; [3]i++)
```
In this very typical example, our [1] initializer initializes an integer value called `i` (this is convention, but you can name this variable anything allowed in C#) with a value of 1. Your initializer MUST initialize a value for the variable (duh). Our [2] condition tells the loop to keep iterating until this variable is equal to the number of bullets we want spawned. Our [3] iterator then simply increases the initialized variable by 1 each iteration. The `i++` syntax is the same as `i += 1` or `i = i + 1`. 

All together, we execute the code inside this loop one time for every bullet we want to spawn, and the `i` variable tells us which bullet we are on, so that we can use that value in our code.

***

<small><img src="CaptainBlasterMini.png" width="30" height="30"> Students, reach out to Jester if you have any further questions about this project. Happy coding!</small>