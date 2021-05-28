# Frontend responsive design
Our goal with the website layout & elements was to provide full-responsiveness regardless of screen size and device. This is something that can vary a lot in difficulty depending on what type of element you're dealing with.
Buttons, text, and other **GUI** elements are rather easy to scale and adjust based on underlying client-side parameters such as screen-size. </br>

This is something we've done quite easily in a lot of places by using [Tailwind CSS](https://tailwindcss.com/) as our CSS-library of choice.
By using Tailwind we can easily design our elements by dividing them up into rows/columns and appyling all the neccessary design adjustments right inside the HTML.

# The canvas
Our main source of attention is derived from the canvas where the actual game is rendered and displayed.</br>
The problem with the canvas and **responsive-design** comes from its rendering nature, **relational** sizing. By relational sizing, I mean the size of all objects inside of the canvas are relative to one another.
For example (not actual sizing), say the canvas is 100px wide, and therefore 100px tall. A token's width might then be 1/20 of the canvas width, as its relative to everything surronding it.
And this goes on and on, font-sizes for example might be 1/100 of the canvas width, and so if the canvas gets bigger, so does the font-size.

This brings us to the **problem** at hand: the canvas width must be **equal** to the height, a 1:1 aspect ratio. Seeing as most devices don't have a 1:1 ratio, but rather most commonly 16:9.</br>
So to combat this the canvas scales semi-responsively, on larger screens it sets itself to a high percentage of the total page area, and on mobile screens it fits as big as possible to combat the smaller screen.


