﻿namespace IT.Base64;

/// <see cref="System.Buffers.OperationStatus"/>
public enum EncodingStatus
{
    Done = 0,
    InvalidDestinationLength = 1,
    InvalidDataLength = 2
}