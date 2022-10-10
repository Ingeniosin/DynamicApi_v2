﻿namespace DynamicApi.Serializers;

public enum SerializeType {
    STANDARD,
    FILE,
    NONE,
}

public static class Serializer {
    private readonly static Dictionary<SerializeType, ISerializer> Serializers = new(){
        { SerializeType.STANDARD, new StandardSerializer() },
        { SerializeType.FILE, new FileSerializer() },
        { SerializeType.NONE, new NoneSerializer() },
    };

    public static IResult Ok(SerializeType serializeType = SerializeType.NONE) {
        return Serialize((object) null, serializeType);
    }

    public static IResult Serialize<TOut>(TOut obj, SerializeType serializeType = SerializeType.STANDARD) {
        try{
            var serializer = Serializers[serializeType];
            return serializer!.Serialize(obj);
        } catch (Exception e){
            return Results.Json(new {
                error = e.Message,
                stackTrace = e.StackTrace,
                innerException = e.InnerException?.Message,
                isValidationException = false,
            }, contentType: "application/json", statusCode: 400);
        }
    }
}