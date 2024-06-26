// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: ClassConverter.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using scg = global::System.Collections.Generic;
namespace PlayerDataProtocol {

  #region Enums
  public enum AnimitorState {
    Idle = 0,
    Move = 1,
    Atk = 2,
  }

  #endregion

  #region Messages
  public sealed class UserData : pb::IMessage {
    private static readonly pb::MessageParser<UserData> _parser = new pb::MessageParser<UserData>(() => new UserData());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<UserData> Parser { get { return _parser; } }

    /// <summary>Field number for the "username" field.</summary>
    public const int UsernameFieldNumber = 1;
    private string username_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Username {
      get { return username_; }
      set {
        username_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "userPassword" field.</summary>
    public const int UserPasswordFieldNumber = 2;
    private string userPassword_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string UserPassword {
      get { return userPassword_; }
      set {
        userPassword_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Username.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Username);
      }
      if (UserPassword.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(UserPassword);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Username.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Username);
      }
      if (UserPassword.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(UserPassword);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            Username = input.ReadString();
            break;
          }
          case 18: {
            UserPassword = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed class PlayerData : pb::IMessage {
    private static readonly pb::MessageParser<PlayerData> _parser = new pb::MessageParser<PlayerData>(() => new PlayerData());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<PlayerData> Parser { get { return _parser; } }

    /// <summary>Field number for the "username" field.</summary>
    public const int UsernameFieldNumber = 1;
    private string username_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Username {
      get { return username_; }
      set {
        username_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "path" field.</summary>
    public const int PathFieldNumber = 2;
    private string path_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Path {
      get { return path_; }
      set {
        path_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "posx" field.</summary>
    public const int PosxFieldNumber = 3;
    private float posx_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float Posx {
      get { return posx_; }
      set {
        posx_ = value;
      }
    }

    /// <summary>Field number for the "posz" field.</summary>
    public const int PoszFieldNumber = 4;
    private float posz_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float Posz {
      get { return posz_; }
      set {
        posz_ = value;
      }
    }

    /// <summary>Field number for the "roty" field.</summary>
    public const int RotyFieldNumber = 5;
    private float roty_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float Roty {
      get { return roty_; }
      set {
        roty_ = value;
      }
    }

    /// <summary>Field number for the "aniState" field.</summary>
    public const int AniStateFieldNumber = 6;
    private global::PlayerDataProtocol.AnimitorState aniState_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::PlayerDataProtocol.AnimitorState AniState {
      get { return aniState_; }
      set {
        aniState_ = value;
      }
    }

    /// <summary>Field number for the "nowHp" field.</summary>
    public const int NowHpFieldNumber = 7;
    private int nowHp_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int NowHp {
      get { return nowHp_; }
      set {
        nowHp_ = value;
      }
    }

    /// <summary>Field number for the "maxHp" field.</summary>
    public const int MaxHpFieldNumber = 8;
    private int maxHp_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int MaxHp {
      get { return maxHp_; }
      set {
        maxHp_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Username.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Username);
      }
      if (Path.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Path);
      }
      if (Posx != 0F) {
        output.WriteRawTag(29);
        output.WriteFloat(Posx);
      }
      if (Posz != 0F) {
        output.WriteRawTag(37);
        output.WriteFloat(Posz);
      }
      if (Roty != 0F) {
        output.WriteRawTag(45);
        output.WriteFloat(Roty);
      }
      if (AniState != 0) {
        output.WriteRawTag(48);
        output.WriteEnum((int) AniState);
      }
      if (NowHp != 0) {
        output.WriteRawTag(56);
        output.WriteInt32(NowHp);
      }
      if (MaxHp != 0) {
        output.WriteRawTag(64);
        output.WriteInt32(MaxHp);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Username.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Username);
      }
      if (Path.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Path);
      }
      if (Posx != 0F) {
        size += 1 + 4;
      }
      if (Posz != 0F) {
        size += 1 + 4;
      }
      if (Roty != 0F) {
        size += 1 + 4;
      }
      if (AniState != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) AniState);
      }
      if (NowHp != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(NowHp);
      }
      if (MaxHp != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(MaxHp);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            Username = input.ReadString();
            break;
          }
          case 18: {
            Path = input.ReadString();
            break;
          }
          case 29: {
            Posx = input.ReadFloat();
            break;
          }
          case 37: {
            Posz = input.ReadFloat();
            break;
          }
          case 45: {
            Roty = input.ReadFloat();
            break;
          }
          case 48: {
            aniState_ = (global::PlayerDataProtocol.AnimitorState) input.ReadEnum();
            break;
          }
          case 56: {
            NowHp = input.ReadInt32();
            break;
          }
          case 64: {
            MaxHp = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  public sealed class OnLinePlayer : pb::IMessage {
    private static readonly pb::MessageParser<OnLinePlayer> _parser = new pb::MessageParser<OnLinePlayer>(() => new OnLinePlayer());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<OnLinePlayer> Parser { get { return _parser; } }

    /// <summary>Field number for the "PlayerData" field.</summary>
    public const int PlayerDataFieldNumber = 1;
    private static readonly pb::FieldCodec<global::PlayerDataProtocol.PlayerData> _repeated_playerData_codec
        = pb::FieldCodec.ForMessage(10, global::PlayerDataProtocol.PlayerData.Parser);
    private readonly pbc::RepeatedField<global::PlayerDataProtocol.PlayerData> playerData_ = new pbc::RepeatedField<global::PlayerDataProtocol.PlayerData>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::PlayerDataProtocol.PlayerData> PlayerData {
      get { return playerData_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      playerData_.WriteTo(output, _repeated_playerData_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += playerData_.CalculateSize(_repeated_playerData_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            playerData_.AddEntriesFrom(input, _repeated_playerData_codec);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
