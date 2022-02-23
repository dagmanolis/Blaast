# Blaast : Blazor App State
A simple and naive implementation for maintaining a global application state in .Net Blazor, based on Castle's DynamicProxy.

## Setup
Create a class with public virtual properties that will be used as a placeholder for all global state variables
``` c#
public class AppStateProperties
{
    public virtual List<string>? Files { get; set; }
    public virtual bool IsFullScreen { get; set; } = true;
    // ...
}
```
Add the namespace to your \_imports.razor file
```c#
@using Blaast
```
Inject Blaast service to your Program.cs and pass the variables placeholder class
``` c#
services.AddBlaastService<AppStateProperties>();
```
At the main component (usually the Layout component) inject the Blaast service and register to the HasChanged event. At the callback use InvokeAsync and StateHasChanged to notify all child components, as well as the main one of course.
``` c#
[Inject]
public BlaastService? BlaastService { get; set; }
//...
protected override void OnInitialized()
{
    if ( BlaastService != null )
        BlaastService.HasChanged += async () => await InvokeAsync( StateHasChanged );
}
```
## Usage
Use the app state variables placeholder at any nested component by injecting the class.
``` c#
[Inject]
public AppStateProperties? AppStateProperties { get; set; }
```
## Important Notice
Due to the naive and simplistic nature of the project you can use only the property's setter method to initiate a HasChanged event. If you use other methods (like Clear() or Add() on a List\<T\>) the event will not fire.
``` c#
//these will fire the event
Files = new List<string>();
IsFullScreen = false;

//these will NOT fire the event
Files.Clear();
Files.Add("filename");
```
