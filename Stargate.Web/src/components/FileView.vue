<template>
  <div id="fileView">
    <div class="box">
      <article class="media">
        <div class="media-left">
          <figure class="image is-64x64" style="margin-top: 5px">
            <i class="fa fa-file-o fa-4x" aria-hidden="true"></i>
          </figure>
        </div>
        <div class="media-content">
          <div class="content">
            <p>
              <strong>{{ fileName }}</strong> <small>{{ calculatedFileSize}}</small>
              <br>
              <div class="field has-addons">
                <p class="control is-expanded">
                  <input class="input file-uri" type="text" :value="fileUri" readonly />
                </p>
                <p class="control">
                  <a class="button tooltip" @click="copyToClipboard">
                    <i class="fa fa-clipboard" aria-hidden="true"></i>
                    <span class="tooltiptext">Copied to clipboard!</span>
                  </a>
                </p>
              </div>
            </p>
          </div>
        </div>
      </article>
    </div>

    <div class="columns">
      <div class="column is-2 is-left">
        <a class="button is-link" @click="uploadAgainClick">
          <span class="file-icon">
              <i class="fa fa-arrow-left"></i>           
            </span>
            Upload Again
        </a>
      </div>

    </div>
  </div>
</template>

<script>
import { FileApi } from "../services/file-upload.service";
import { FORMATTER } from "../services/formatter.service";
import Router from "../router";

const SERVER_UNAVAILABLE = "Unable to reach the upload service. Please be sure the service is running, and that the hostname and port are properly configured."

export default {
  name: "file-view",
  data() {
    return {
      fileUri: null,
      fileName: null,
      fileSizeBytes: null,
      id: this.$route.params.id
    };
  },
  computed: {
    calculatedFileSize: function() {
      return FORMATTER.bytesToString(this.fileSizeBytes);
    }
  },
  created() {
    let self = this;
    FileApi.getById(this.id)
      .then(function(response) {
        self.fileUri = response.data.externalUri;
        self.fileName = response.data.fileName;
        self.fileSizeBytes = response.data.fileSizeBytes;
      })
      .catch(function (error) {
        if (!error.status) {
            self.$alert.danger({ 
              duration: false, 
              message: SERVER_UNAVAILABLE})
        }
        console.log(error);
      });
  },
  mounted () {
    this.$alert.hide()
  },
  methods: {
    copyToClipboard(e) {
      var copyTextarea = document.querySelector(".file-uri");
      copyTextarea.select();
      document.execCommand("copy");
      copyTextarea.blur();

      let self = this;
      self.showCopyToolTip();

      setTimeout(function() {
        self.removeCopyToolTip();
      }, 5000);
    },
    showCopyToolTip() {
      var tooltip = document.getElementsByClassName("tooltiptext");
      tooltip[0].classList.add("tooltip-visible");
    },
    removeCopyToolTip() {
      var tooltip = document.getElementsByClassName("tooltiptext");
      tooltip[0].classList.remove("tooltip-visible");
    },
    uploadAgainClick(e) {
      Router.push({ name: "Upload" });
    }
  }
};
</script>

<style lang="scss" scoped>
.inputless {
  border: 0px;
  height: 48px;
  border-right: 1px solid #dbdbdb;
}
.is-left {
  text-align: left;
}
</style>

