using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ErrorResponse
{
    public Error error;

}
[Serializable]
public class Error
{
    public string text;
    public int http_code;
    public string error_code;
    public string request;
}