namespace AjObjects
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    internal enum SerializerType : byte
    {
        BasicObject = 0,
        Byte = 1,
        Char = 2,
        Short = 3,
        Integer = 4,
        Long = 5,
        Float = 6,
        Double = 7,
        Decimal = 8,
        DateTime = 9,
        Guid = 10,
        String = 11,
        List = 12,
        Null = 13
    }

    public class BasicObjectSerializer
    {
        public void Serialize(BasicObject obj, Stream output)
        {
            BinaryWriter writer = new BinaryWriter(output, Encoding.Unicode);
            this.SerializeBasicObject(writer, obj);
        }

        public BasicObject Deserialize(Stream input)
        {
            BinaryReader reader = new BinaryReader(input, Encoding.Unicode);
            byte type = reader.ReadByte();

            if (type != (byte) SerializerType.BasicObject)
                throw new InvalidDataException("Not a BasicObject");

            return this.DeserializeBasicObject(reader);
        }

        private BasicObject DeserializeBasicObject(BinaryReader reader)
        {
            int count = reader.ReadInt32();

            BasicObject obj = new BasicObject();

            for (int k = 0; k < count; k++)
            {
                string name = reader.ReadString();
                object value = this.ReadValue(reader);

                obj[name] = value;
            }

            return obj;
        }

        private void WriteValue(BinaryWriter writer, object value)
        {
            if (value == null)
            {
                writer.Write((byte)SerializerType.Null);
                return;
            }

            if (value is string)
            {
                writer.Write((byte)SerializerType.String);
                writer.Write((string)value);
                return;
            }

            if (value is int)
            {
                writer.Write((byte)SerializerType.Integer);
                writer.Write((int)value);
                return;
            }

            if (value is BasicObject)
            {
                this.SerializeBasicObject(writer, (BasicObject)value);
                return;
            }

            if (value is System.Guid)
            {
                writer.Write((byte)SerializerType.Guid);
                writer.Write(((System.Guid)value).ToByteArray());
                return;
            }

            if (value is DateTime)
            {
                writer.Write((byte)SerializerType.DateTime);
                writer.Write(((DateTime)value).ToBinary());
                return;
            }

            throw new InvalidDataException(string.Format("Not serializable value of type {0}", value.GetType().Name));
        }

        private void SerializeBasicObject(BinaryWriter writer, BasicObject obj)
        {
            writer.Write((byte)SerializerType.BasicObject);
            ICollection<string> names = obj.Names;
            writer.Write(names.Count);

            foreach (string name in names)
            {
                writer.Write(name);
                this.WriteValue(writer, obj[name]);
            }
        }

        private object ReadValue(BinaryReader reader)
        {
            byte type = reader.ReadByte();

            switch (type)
            {
                case (byte) SerializerType.Byte:
                    return reader.ReadByte();
                case (byte) SerializerType.Integer:
                    return reader.ReadInt32();
                case (byte) SerializerType.String:
                    return reader.ReadString();
                case (byte) SerializerType.BasicObject:
                    return this.DeserializeBasicObject(reader);
                case (byte) SerializerType.Guid:
                    return new System.Guid(reader.ReadBytes(16));
                case (byte) SerializerType.DateTime:
                    return DateTime.FromBinary(reader.ReadInt64());
            }

            throw new InvalidDataException("Invalid data deserializing BasicObject");
        }
    }
}
