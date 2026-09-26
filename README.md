# ⚪ **Overview**
**This is a native windows app**, that has got one and only purpose - help you keep a better track of PC Components.


# Add, View in Detail and Edit your components!
This app allows you to **create** [Image/Gif]
**View in Detail** [Gif]
And **Edit** [Gif]

## ➕ Adding components
You can **add** in **your components using the huge "+" button**, located in the main window, the window shown right after you open the app.
<p><img width="184" height="171" alt="Snímek obrazovky 2026-09-26 174225" src="https://github.com/user-attachments/assets/2aacef75-16b5-46e0-825c-85d2fe2399e2"/><p/>
Once there, you can start out by adding the name of the parameter, by clicking on the huge textbox right on top of that window.
With that done, **assign any parameters** you like **to the** new **component**.
Literaly, as you can [make your own parameters]-link too.
After you're done assigning the values to your component, you can click Save to save the component.

## 🔍 Viewing Components in Detail
Now that a component is there, you can **view it by clicking on Find parameter button** in the main window. <p><img width="162" height="162" alt="Snímek obrazovky 2026-09-26 175116" src="https://github.com/user-attachments/assets/66f09798-2648-4584-ae30-6ff1971f1dfe"/><p/>
If you've set a type for your component, you can just click on any of the buttons to find any component of that type you've added instantly. To view all of the components, just click on the little 🔍 icon in the top-left corner.
This will show you all of the components you've added so far, as well as a few operations you can do with them. For now we're interested in the "Detail" button. <p><img width="93" height="47" alt="Snímek obrazovky 2026-09-26 175219" src="https://github.com/user-attachments/assets/7fc4cc06-19bd-4eb6-b27a-36e3c9142bdd"/><p/>
 This will take you to the Detail window, from where you can view all of the details of the component you clicked on.

## ✏️ Editing Components
Enjoyed looking at your Components but noticed something off? Not to worry, the "Edit" button is here to save the day. <p><img width="53" height="32" alt="Snímek obrazovky 2026-09-26 180025" src="https://github.com/user-attachments/assets/0c7e22b4-499c-4a3f-8014-77b35016b3a2"/> <p/> You can find it next to the Detail button and upon clicking on it a window similar to the one of Detail will pop-up. But this time it's the Edit window. [Image] 
On the right side of this window, you can **edit any of the parameters** you've previously assigned to the parameter, as well as add any parameters you might've forgotten to add. [Image]

On the left side, you can **upload a main photo for the component**, as well as an **unlimited amount of other photos** for the component, all which you can [cycle through] -link. [Image]
And don't worry, you can delete your photos of the components after you've added them here, as the app keeps a copy of each image in case they would get lost. You can also remove them by using the little red cross.
On the bottom of this window, you will find a 
### **Mark-down style editor**
In which you can write custom descriptions for your components. You write on the left and see how the text will look like on the right. [Image]
Now in case you don't really know how to write in Mark-down, you can use the built-in buttons just above the description box. Just select the text you wish to make changes to and click on any of the buttons. [Gif]
Currently the app supports: **Bold**, *Italic*, ***Bold Italic*** and two Headings styles.
The descriptions will be rendered in the Detail window as well.

## ❌ Deleting Components
You can of course **delete your components**, using the conveniently placed delete button next to the edit and detail buttons.
Now I'm sure you weren't expecting this one, but deleting a component will delete it. What a shocker, I know.

# 🔎 Finding and Filtering components
In case you actually want to use this app and have already added a bazzilion components and now cannot find your desired component, it's time to use the built-in search and filtering.
### Searching components
This is the first and simpler way to find components. In the window that contains the list of all your components, just click on the search bar and start typing what component you're in search for. The app will then search through all your components and check if their name matches. [Gif]

### Filtering components
If searching up just by the name of a component isn't enough for you, the **Filter window** is. You can open it using the little funnel icon still in the same window, which will open it. [Image]
In the filter window, you can toggle pretty much any of the parameters and whatever you pick out will be what is filtered. Thus if you choose the type parameter to be "CPU", the app will only show you components that have the type parameter of "CPU". Same goes for all other parameters. You can clear that specific filter by clicking the tiny red x next to the parameter.
[Gif]
When we were at clearing filters, you can click the big red X in the window where all components are listed to clear all current filters.
Now when it comes to **number type parameters**, such as the "Capacity" parameter you will see **three different** icons next to the type bar. These will toggle the **filtering mode** for that parameter, of which there are three in total. These are: **Equals, Bigger and Smaller**. If you set the value (for example here) of "Capacity" to 500 and choose the Equals mode, then only components with capacity of 500 will show up. If you choose the bigger mode, only components whose capacity is greater than 500 or same will be shown. The smaller mode works the exact same way, just as a direct opposite. And if you choose none, the equals mode will be used by default.
You can also filter out your..
## 💗 Favorite components
You can toggle the little heart icon next to your components, which will make them "favorited". This does nothing extra on its own, it's an indicator purely for you.
The only thing changed is, that you can filter your components by favorite, which will hide all non-favorited components.

# 🖼️ Photo gallery
As funny as this sounds, yes this app has a tiny and simple built in photo-gallery to it. Whenever you **click on any of the components photos** a new Photo window will pop-up. On the sides of this window you can click the arrows to cycle through photos of the component.

# Adding, editing and managing Parameters
## ➕ Adding a Parameter
Got tired of the basic pre-generated parameters I've included? You can add in any and as many as you like parameters to the app. This was the whole purpose of the Expandible system I've built in the app. To start, go ahead and open the Parameters window, using the button I've place in the main window. [Image]
From there the Parameters window will pop-up. In here you can mess around with the pre-generated parameters, as well as your own.
To make a new Parameter, just click on the "+" button you'll see there. This will add in a new parameter and open the window used to..
## 🖊️ Edit a Parameter
Opened, when making a new parameter, or clicking the "Edit" button in the Parameters list.
You can **change**:
- **Name** of the parameter
- The **ID**, but I **never reccomend changing it on older and already used parameters, it WILL cause issues!**
- **Type of the parameter**, which is important that you understand. The parameter can either be a "String" type, or a "Number" type.
    - **String-type** parameters are anything written in text, such as *Model, Manufacturer, Name*, etc.
    - **Number-type** parameters are parameters represented by numbers, such as *Capacity, Power, or VRAM* on a GPU.
- Despite it being an option **NEVER change the type on parameters** you are already using somewhere, as it will break your list of components and the app will refuse to load. Make sure to remove the parameter from all components first, then maybe think about switching this. If you're making a new parameter, you can ignore this warning entirely, as a brand new parameter is not used anywhere.
### **Extra options**
**String**
- **List** - instead of just an empty typing box where you can enter a value a selectable list of values will be placed. You can [tweak these values] too of course.
- **Allow custom entries** - now instead of having a fixed list, you can also enter your own values. Useful for parameters where you can expect to ussualy have a few values, but other values can occur as well.
**Numbers**
- **Custom sufix** - when you have a parameter such as "Capacity" and set its value to 500 on a component, you will get just "Capacity: 500" written everywhere. *500 what? Potatoes?* Custom sufix adds in a sufix everywhere this parameter is displayed and will make the app show "Capacity: 500 GB". That is of course if you set it to GB.
### Tweaking values in case of Lists
You can **add** in new values using the plus button.
**Change the value** by selecting it, changing the name and hitting the little pencil icon. [Gif]
Or just **delete** a value entirely, by clicking the red x next to it.
## 🗑️ Delete a parameter
Now if you get tired of some parameter, no longer use it, or just don't want it, you can **get rid of them**.
Just **press the "Delete" button** in the parameters list and it's gone for good.

# ⚙️ Technicalities
## Components and Parameters saving
**Components and Parameters are saved**, so closing the app WILL NOT erase your data. You can locate the files location under `C:/Users/YourUser/AppData/Roaming/Pc_Parts_Lister`
**Components are stored in the `components.json` file and Possible parameters in `parameters.json`
Each component you add takes up about ~1KB in space, the same goes for Parameters.
This can come in handy in case you ever mess up big-time, as you can just tweak values manually in these files.
And you can do so in any basic code editors, since they're just a normal json files.
### Reccomended hardware
(Mostly included as a joke, the app is super lightweight)
**CPU** - Pretty much any cpu that can run atleast Windows 10
**RAM** - I've never managed to use more than 250 MB at once, so that should do.
