@import ui;

local foo = ui:createInterface("foo")
local d = foo:createComponent("img")
local s = foo:createComponent("sprite")

s:loadGfx("C:\\Users\\astec\\Pictures\\Saved Pictures\\Azenis1_1600.jpg");
d:setSprite(s);
d.transform:setSize(250,250);
s:loadGfx("C:\\Users\\astec\\Pictures\\Saved Pictures\\905926726.jpg");