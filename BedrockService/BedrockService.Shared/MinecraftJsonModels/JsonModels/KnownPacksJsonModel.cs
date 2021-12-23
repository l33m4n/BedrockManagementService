﻿using System;
using System.Collections.Generic;

namespace BedrockService.Shared.MinecraftJsonModels.JsonModels {
    public class KnownPacksJsonModel {
        public int file_version { get; set; }
        public string file_system { get; set; }
        public bool? from_disk { get; set; }
        public List<string> hashes { get; set; }
        public string path { get; set; }
        public string uuid { get; set; }
        public string version { get; set; }

        public override bool Equals(object obj) {
            return obj is KnownPacksJsonModel model &&
                   uuid == model.uuid;
        }

        public override int GetHashCode() {
            return HashCode.Combine(uuid);
        }

        public override string ToString() {
            return path;
        }
    }
}
