# Welcome to Tween Component

<img title="" src="https://github.com/Juce-Assets/Juce-TweenPlayer/blob/develop/Misc/Logo.png?raw=true" alt="Logo" data-align="inline">

**Welcome to [Tween Component](https://github.com/Juce-Assets/Juce-TweenPlayer):** an interpolation animation tool aimed to increase your productivity. It allows you to easily create complex tween sequences directly from the editor. No boilerplate, no coding, just plain fun!

- **Easy to use**: just add the Tween Player component to any GameObject, and start animating properties!

- **Artists ready**: allow your artists to easily create animations directly from the Unity editor, with an intuitive UI and simple controls. They can even animate properties from custom scripts without the help of a coder.

- **Flexible**: nest as many Tween Components as you want to create complex animations.

- **Extendible**: Easily create neew Tween Components, with any custom behaviour imaginable.  

- **Powerful**: Custom Tweening engine that performs blazingly fast.

- **Coders friendly**: we take a lot of effort making sure the underlying structure is clean and easy to extend. Code aims to be robust and well written.

# Contents

- [Why](https://github.com/Juce-Assets/Juce-TweenPlayer#why)
- [Basic Usage](https://github.com/Juce-Assets/Juce-TweenPlayer#basic-usage)
- [Want to contribute?](https://github.com/alichtman/shallow-backup#want-to-contribute)
- [Contributors](https://github.com/alichtman/shallow-backup#contributors)

### Why?

I wanted a tool that allowed teams to:

- Add as much juice as they wanted, without compromising the code architecture.

- Give artists a tool that is easy to use, and that does not depend on coders.

- Expand the existing codebase with not much effort.

- Easily bind custom data.
  
  

### Basic Usage

1. Add the Tween Player Component to any GameObject. GameObjects can have more than one Tween Player.
   
   ![](https://github.com/Juce-Assets/Juce-TweenPlayer/blob/develop/Misc/Readme1.png?raw=true)

2. Add Tween Components with the Add Component button.
   
   <img title="" src="https://github.com/Juce-Assets/Juce-TweenPlayer/blob/develop/Misc/Readme2.png?raw=true" alt="" data-align="inline">
   
   Add as many as you want until the desired animation is reached.
   
   ![](https://github.com/Juce-Assets/Juce-TweenPlayer/blob/develop/Misc/Readme3.png?raw=true)

3. Play!
- Directly from the editor:
  
  ![](https://github.com/Juce-Assets/Juce-TweenPlayer/blob/develop/Misc/Readme4.png?raw=true)
  
  

- Through script:
  
  ```csharp
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using UnityEngine;
  using Juce.TweenPlayer;
  
  namespace Assets
  {
      public class PlayExample : MonoBehaviour
      {
          [SerializeField] private TweenPlayer tweenPlayer = default;
  
          private void Start()
          {
              // Plays the sequence
              tweenPlayer.Play();
  
              // Instantly stops the sequence
              tweenPlayer.Kill();
  
              // Instantly reaches end of sequence
              tweenPlayer.Complete();
          }
      }
  }
  ```



### Want to contribute?

**Please follow these steps to get your work merged in.**

0. Clone the repo and make a new branch: `$ git checkout https://github.com/Juce-Assets/Juce-TweenPlayer/tree/develop -b [name_of_new_branch]`.

1. Add a feature, fix a bug, or refactor some code :)

2. Update `README.md` contributors, if necessary.

3. Open a Pull Request with a comprehensive description of changes.



### Contributors

- Guillem Sunyer 
