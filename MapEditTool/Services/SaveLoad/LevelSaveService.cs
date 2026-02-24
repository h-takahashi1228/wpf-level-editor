using MapEditTool.Models.Map.ObjectData;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.IO;

namespace MapEditTool.Services.SaveLoad
{
    internal class LevelSaveService
    {
        private readonly JsonSerializerOptions _options;

        public LevelSaveService()
        {
            _options = new JsonSerializerOptions
            {
                WriteIndented = true,
                TypeInfoResolver = new DefaultJsonTypeInfoResolver
                {
                    Modifiers = { PolymorphismModifier }
                }
            };

            // enumを文字列で保存
            _options.Converters.Add(new JsonStringEnumConverter());
        }

        /// ObjectDataの派生クラスをJSONに保存できるようにする
        private void PolymorphismModifier(JsonTypeInfo typeInfo)
        {
            if (typeInfo.Type == typeof(ObjectData))
            {
                typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                {
                    TypeDiscriminatorPropertyName = "$type"
                };

                typeInfo.PolymorphismOptions.DerivedTypes.Add(
                    new JsonDerivedType(typeof(PlayerSpawnerData), "PlayerSpawner"));

                typeInfo.PolymorphismOptions.DerivedTypes.Add(
                    new JsonDerivedType(typeof(DefenseBaseData), "DefenseBase"));

                typeInfo.PolymorphismOptions.DerivedTypes.Add(
                    new JsonDerivedType(typeof(EnemySpawnerData), "EnemySpawner"));
            }
        }

        /// LevelDataをJSON保存
        public void Save(string path, LevelData level)
        {
            var json = JsonSerializer.Serialize(level, _options);
            File.WriteAllText(path, json);
        }

        /// JSONからLevelDataを復元
        public LevelData Load(string path)
        {
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<LevelData>(json, _options)
                   ?? throw new Exception("Failed to load level.");
        }
    }
}
