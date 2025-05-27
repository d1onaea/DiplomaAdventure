EXTERNAL ChooseItem()

//GAME BEGINNING
=== game_beginning ===
I still find it hard to accept that father is no longer with us. 
You probably don’t remember this place very well. 
You used to hate it when you were a child.
Now it's all yours.
*[Why didn't granpa leave this place to you?]
Because he needed to care for HER son.
**[Why don't you say HER name?] 
We’ve already discussed this. 
->take_a_look_around
**[You mean my mom?]
Yes, I'm sorry. I mean your mom. 
->take_a_look_around

=== take_a_look_around
*[You kinda look VERY uncomfortable]
Nonsense.
Why are you just standing there? Take a look around!
If you have any questions, you can find me here.
->END

//GAME BEGINNING

=== show_evidence === 
Huh? Do you want to show me something? 
* [No] 
    I'll stay here if you need me.
* [Yes] 
    ~ ChooseItem()
- ->END

// REACTING TO ITEMS
=== empty_inventory === 
You are not carrying anything, Oscar!
->END 

//CLUES
===diary_collected===
Nina...
*[What's that supposed to mean?]
I've made a huge mistake when I was younger, Oscar.
I don't think you should read too much into it. 
**[Are you hiding something from me?]
...
->END 

===bottle_collected===
For my sweet litte...?
*[I don't think they meant granpa, Sara]
Yes. And don't look at me like that, I have nothing to tell you.
->END

===photograph_collected===
This is the last time that I saw her this happy.
*[Was she not happy after that?]
I... don't know. 
->END

===folder_collected===
...I can't believe he didn't get rid of that! 
*[Why is the date listed a year BEFORE mom's death?]
A typo. Dad was bad with dates, haha.
**[You're hiding something, Sara.]
Everyone has skeletons in the closet.
-> END

===drawing_collected===
Oh, it's... um... a picture you drew!
*[I don't remember drawing it.]
You were pretty young, buddy.
**[Were you guys fighting? Why are you holding my hand?]
Oscar, I don't remember. It was more than 20 years ago!
-> END

//JUNK
===mug_collected===
Huh? Why are you showing me a mug? 
Oscar don't waste my time with this rubbish!
->END 

===vase_collected===
Um... Yeah, I see that's a vase. 
So what?
->END

===stapler_collected===
Oh, I don't need a stapler, thank you.
->END

=== player_won === 
Alright, alright. You deserve to know. 
She tried to do better.
*[What are you talking about?]
Your dad left Nina right after she gave birth to you.
Nina met a shady guy. He was a drug dealer. 
**[So she became an addict?..]
Yes. Nobody knew. 
But she wanted to change.
***[And then?]
And then I found out. I was horrified. 
I told dad everything and took you away.
Nina and I had a fight. She asked me why I was doing it to her. 
I told her I couldn't trust a junkie. 
****[Don't tell me-...]
She relapsed and got in the driver's seat. You know the rest.
I'm sorry Oscar. You must be angry at me but all I wanted was you to be safe.  
->END

===player_lost===
Stop. I don't know why you don't trust me. 
You're just wasting my time by bringing me this junk. 
Is that a prank? I lost my dad, Oscar. 
I can't do it anymore. I'm going back to work. 
Bye. 
->END



