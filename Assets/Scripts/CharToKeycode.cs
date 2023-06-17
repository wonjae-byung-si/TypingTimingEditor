using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharToKeycode
{
    public static Dictionary<char, KeyCode> chartoKeycode = new Dictionary<char, KeyCode>()
    {
      //-------------------------LOGICAL mappings-------------------------
  
      //Lower Case Letters
      {'a', KeyCode.A},
      {'b', KeyCode.B},
      {'c', KeyCode.C},
      {'d', KeyCode.D},
      {'e', KeyCode.E},
      {'f', KeyCode.F},
      {'g', KeyCode.G},
      {'h', KeyCode.H},
      {'i', KeyCode.I},
      {'j', KeyCode.J},
      {'k', KeyCode.K},
      {'l', KeyCode.L},
      {'m', KeyCode.M},
      {'n', KeyCode.N},
      {'o', KeyCode.O},
      {'p', KeyCode.P},
      {'q', KeyCode.Q},
      {'r', KeyCode.R},
      {'s', KeyCode.S},
      {'t', KeyCode.T},
      {'u', KeyCode.U},
      {'v', KeyCode.V},
      {'w', KeyCode.W},
      {'x', KeyCode.X},
      {'y', KeyCode.Y},
      {'z', KeyCode.Z},
  
      //KeyPad Numbers
      {'1', KeyCode.Keypad1},
      {'2', KeyCode.Keypad2},
      {'3', KeyCode.Keypad3},
      {'4', KeyCode.Keypad4},
      {'5', KeyCode.Keypad5},
      {'6', KeyCode.Keypad6},
      {'7', KeyCode.Keypad7},
      {'8', KeyCode.Keypad8},
      {'9', KeyCode.Keypad9},
      {'0', KeyCode.Keypad0},
  
      //Other Symbols
      {'!', KeyCode.Exclaim}, //1
      {'"', KeyCode.DoubleQuote},
      {'#', KeyCode.Hash}, //3
      {'$', KeyCode.Dollar}, //4
      {'&', KeyCode.Ampersand}, //7
      {'\'', KeyCode.Quote}, //remember the special forward slash rule... this isnt wrong
      {'(', KeyCode.LeftParen}, //9
      {')', KeyCode.RightParen}, //0
      {'*', KeyCode.Asterisk}, //8
      {'+', KeyCode.Plus},
      {',', KeyCode.Comma},
      {'-', KeyCode.Minus},
      {'.', KeyCode.Period},
      {'/', KeyCode.Slash},
      {':', KeyCode.Colon},
      {';', KeyCode.Semicolon},
      {'<', KeyCode.Less},
      {'=', KeyCode.Equals},
      {'>', KeyCode.Greater},
      {'?', KeyCode.Question},
      {'@', KeyCode.At}, //2
      {'[', KeyCode.LeftBracket},
      {'\\', KeyCode.Backslash}, //remember the special forward slash rule... this isnt wrong
      {']', KeyCode.RightBracket},
      {'^', KeyCode.Caret}, //6
      {'_', KeyCode.Underscore},
      {'`', KeyCode.BackQuote},
  
      //-------------------------NON-LOGICAL mappings-------------------------
  
      //NOTE: all of these can easily be remapped to something that perhaps you find more useful
  
      //---Mappings where the logical keycode was taken up by its counter part in either (the regular keybaord) or the (keypad)
  
      //Alpha Numbers
      //NOTE: we are using the UPPER CASE LETTERS Q -> P because they are nearest to the Alpha Numbers
      {'Q', KeyCode.Alpha1},
      {'W', KeyCode.Alpha2},
      {'E', KeyCode.Alpha3},
      {'R', KeyCode.Alpha4},
      {'T', KeyCode.Alpha5},
      {'Y', KeyCode.Alpha6},
      {'U', KeyCode.Alpha7},
      {'I', KeyCode.Alpha8},
      {'O', KeyCode.Alpha9},
      {'P', KeyCode.Alpha0},
  
      //INACTIVE since I am using these characters else where
      {'A', KeyCode.KeypadPeriod},
      {'B', KeyCode.KeypadDivide},
      {'C', KeyCode.KeypadMultiply},
      {'D', KeyCode.KeypadMinus},
      {'F', KeyCode.KeypadPlus},
      {'G', KeyCode.KeypadEquals},
  
      //-------------------------CHARACTER KEYS with NO KEYCODE-------------------------
  
      //NOTE: you can map these to any of the OPEN KEYCODES below
  
      /*
      //Upper Case Letters (16)
      {'H', -},
      {'J', -},
      {'K', -},
      {'L', -},
      {'M', -},
      {'N', -},
      {'S', -},
      {'V', -},
      {'X', -},
      {'Z', -}
      */
  
      //-------------------------KEYCODES with NO CHARACER KEY-------------------------
  
      //-----KeyCodes without Logical Mappings
      //-Anything above "KeyCode.Space" in Unity's Documentation (9 KeyCodes)
      //-Anything between "KeyCode.UpArrow" and "KeyCode.F15" in Unity's Documentation (24 KeyCodes)
      //-Anything Below "KeyCode.Numlock" in Unity's Documentation [(28 KeyCodes) + (9 * 20 = 180 JoyStickCodes) = 208 KeyCodes]
  
      //-------------------------other-------------------------

      //-----KeyCodes that are inaccesible for some reason
      //{'~', KeyCode.tilde},
      //{'{', KeyCode.LeftCurlyBrace}, 
      //{'}', KeyCode.RightCurlyBrace}, 
      //{'|', KeyCode.Line},   
      //{'%', KeyCode.percent},
    };
}
