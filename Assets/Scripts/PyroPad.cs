using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System.Collections.Generic;

public enum button{A, B, X, Y, Start, Select, LeftStick, RightStick, LeftShoulder, RightShoulder, Left, Right, Up, Down}
public enum trigger{Left, Right};
public enum stick{Left, Right};

public enum playerIndex { One, Two, Three, Four};

public class PyroPad : GamePad {

    public Vector2 RightStick { get { return GetAxis(stick.Right); } }
    public Vector2 LeftStick { get { return GetAxis(stick.Left); } }

    public float LeftTrigger { get { return GetTrigger(trigger.Left); } }
    public float RightTrigger { get { return GetTrigger(trigger.Right); } }

    private PlayerIndex index;
    private GamePadState current, last;


    public PyroPad(playerIndex index)
    {
        this.index = (PlayerIndex)index;
    }

    public void Update()
    {
        last = current;
        current = GamePad.GetState(index);
    }

      
    public bool GetButtonDown( button butt)
    {

        switch (butt)
        {
            case button.A:
                return last.Buttons.A == ButtonState.Released && current.Buttons.A == ButtonState.Pressed;
                
            case button.B:
                return last.Buttons.B == ButtonState.Released && current.Buttons.B == ButtonState.Pressed;
              
            case button.X:
                return last.Buttons.X == ButtonState.Released && current.Buttons.X == ButtonState.Pressed;
            
            case button.Y:
                return last.Buttons.Y == ButtonState.Released && current.Buttons.Y == ButtonState.Pressed;
               
            case button.Start:
                return last.Buttons.Start == ButtonState.Released && current.Buttons.Start == ButtonState.Pressed;
               
            case button.Select:
                return last.Buttons.Back == ButtonState.Released && current.Buttons.Back == ButtonState.Pressed;
                
            case button.LeftStick:
                return last.Buttons.LeftStick == ButtonState.Released && current.Buttons.LeftStick == ButtonState.Pressed;
                
            case button.RightStick:
                return last.Buttons.RightStick == ButtonState.Released && current.Buttons.RightStick == ButtonState.Pressed;
                
            case button.LeftShoulder:
                return last.Buttons.LeftShoulder == ButtonState.Released && current.Buttons.LeftShoulder == ButtonState.Pressed;
               
            case button.RightShoulder:
                return last.Buttons.RightShoulder == ButtonState.Released && current.Buttons.RightShoulder == ButtonState.Pressed;

            case button.Up:
                return last.DPad.Up == ButtonState.Released && current.DPad.Up == ButtonState.Pressed;

           case button.Down:
                return last.DPad.Down == ButtonState.Released && current.DPad.Down == ButtonState.Pressed;

           case button.Left:
                return last.DPad.Left == ButtonState.Released && current.DPad.Left == ButtonState.Pressed;

           case button.Right:
                return last.DPad.Right == ButtonState.Released && current.DPad.Right == ButtonState.Pressed;

            default:
                return false;

           
        }
      
        
    }
    public bool GetButtonUp(button butt)
    {
        switch (butt)
        {
            case button.A:
                return last.Buttons.A == ButtonState.Pressed && current.Buttons.A == ButtonState.Released;

            case button.B:
                return last.Buttons.B == ButtonState.Pressed && current.Buttons.B == ButtonState.Released;

            case button.X:
                return last.Buttons.X == ButtonState.Pressed && current.Buttons.X == ButtonState.Released;

            case button.Y:
                return last.Buttons.Y == ButtonState.Pressed && current.Buttons.Y == ButtonState.Released;

            case button.Start:
                return last.Buttons.Start == ButtonState.Pressed && current.Buttons.Start == ButtonState.Released;

            case button.Select:
                return last.Buttons.Back == ButtonState.Pressed && current.Buttons.Back == ButtonState.Released;

            case button.LeftStick:
                return last.Buttons.LeftStick == ButtonState.Pressed && current.Buttons.LeftStick == ButtonState.Released;

            case button.RightStick:
                return last.Buttons.RightStick == ButtonState.Pressed && current.Buttons.RightStick == ButtonState.Released;

            case button.LeftShoulder:
                return last.Buttons.LeftShoulder == ButtonState.Pressed && current.Buttons.LeftShoulder == ButtonState.Released;

            case button.RightShoulder:
                return last.Buttons.RightShoulder == ButtonState.Pressed && current.Buttons.RightShoulder == ButtonState.Released;

            case button.Up:
                return last.DPad.Up == ButtonState.Pressed && current.DPad.Up == ButtonState.Released;

           case button.Down:
                return last.DPad.Down == ButtonState.Pressed && current.DPad.Down == ButtonState.Released;

           case button.Left:
                return last.DPad.Left == ButtonState.Pressed && current.DPad.Left == ButtonState.Released;

           case button.Right:
                return last.DPad.Right == ButtonState.Pressed && current.DPad.Right == ButtonState.Released;

            default:
                return false;
        }
     
    }

    public bool GetButton(button butt)
    {
        switch (butt)
        {
            case button.A:
                return current.Buttons.A == ButtonState.Pressed;

            case button.B:
                return current.Buttons.B == ButtonState.Pressed;

            case button.X:
                return current.Buttons.X == ButtonState.Pressed;

            case button.Y:
                return current.Buttons.Y == ButtonState.Pressed;

            case button.Start:
                return current.Buttons.Start == ButtonState.Pressed;

            case button.Select:
                return current.Buttons.Back == ButtonState.Pressed;

            case button.LeftStick:
                return current.Buttons.LeftStick == ButtonState.Pressed;

            case button.RightStick:
                return current.Buttons.RightStick == ButtonState.Pressed;

            case button.LeftShoulder:
                return current.Buttons.LeftShoulder == ButtonState.Pressed;

            case button.RightShoulder:
                return current.Buttons.RightShoulder == ButtonState.Pressed;

           case button.Up:
                return current.DPad.Up == ButtonState.Pressed;

           case button.Down:
                return current.DPad.Down == ButtonState.Pressed;

           case button.Left:
                return current.DPad.Left == ButtonState.Pressed;

           case button.Right:
                return current.DPad.Right == ButtonState.Pressed;

            default:
                return false;
        }
       
    }

    public Vector2 GetAxis(stick s)
    {
        switch (s)
        {
            case stick.Left:
                return current.ThumbSticks.Left.vector();
                
            case stick.Right:
                return current.ThumbSticks.Right.vector();
                
            default:
                return Vector2.zero;
         
        }
    }

    public float GetTrigger(trigger t)
    {
        switch (t)
        {
            case trigger.Left:
                return current.Triggers.Left;
                
            case trigger.Right:
                return current.Triggers.Right;
                
            default:
                return 0;
                
        }
    }

    public void vibrate(float left, float right)
    {
        SetVibration(index, left, right);
    }

    public IEnumerator vibrateTime(float left, float right, float time) 
    {
        vibrate(left, right);
        yield return new WaitForSeconds(time);
        vibrate(0, 0);
    }

    
}
