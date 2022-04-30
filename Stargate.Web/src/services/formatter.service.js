export const FORMATTER = {
  bytesToString: function (bytes) {
    if (bytes < 1024) {
      return bytes + ' B';
    }

    if (bytes >= 1024 && bytes < Math.pow(1024, 2)) {
      return Math.round((bytes / 1024) * 10) / 10 + ' kB';
    }

    if (bytes >= Math.pow(1024, 2) && bytes < Math.pow(1024, 3)) {
      return Math.round((bytes / Math.pow(1024, 2)) * 10) / 10 + ' MB';
    }

    if (bytes >= Math.pow(1024, 3) && bytes < Math.pow(1024, 4)) {
      return Math.round((bytes / Math.pow(1024, 3)) * 10) / 10 + ' GB';
    }

    return bytes + ' B';
  }
}
