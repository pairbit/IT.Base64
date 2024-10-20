namespace IT.Base64;

/// <see cref="System.Buffers.OperationStatus"/>
public enum DecodingStatus
{
    Done = 0,
    InvalidDestinationLength = 1,
    InvalidDataLength = 2,
    InvalidData = 3
}