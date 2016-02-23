var gulp = require('gulp');
var sourcemaps = require('gulp-sourcemaps');
var $ = require("gulp-load-plugins")({ pattern: ["gulp-*", "gulp.*", "del*", "path*", "stream-series"], lazy: true });


var filenames = {
  min: {
    appJs: "app.js",
    appCss: "app.css",
    vendorJs: "vendor.js",
    vendorCss: "vendor.css"
  }
};



var paths = {
  dist: {
    debug: ".",
    release: "build",
  },

  src: {
    appIndex: "index.html",
    appJs: "wwwroot/app/**/*.js",
    appCss: "wwwroot/app/*.css",
    appFonts: [
      "wwwroot/lib/**/*.eot",
      "wwwroot/lib/**/*.svg",
      "wwwroot/lib/**/*.ttf",
      "wwwroot/lib/**/*.woff"
    ],
    appImages: [
      "wwwroot/**/*.png",
      "wwwroot/**/*.jpg",
      "wwwroot/**/*.ico"
    ],
    appNgTemplates: "wwwroot/app/**/*.html",
    vendorJs: [
      "wwwroot/lib/moment/min/moment.min.js",
      "wwwroot/lib/jquery/dist/jquery.min.js",
      "wwwroot/lib/lodash/dist/lodash.min.js",
      "wwwroot/lib/angular/angular.js",
      "wwwroot/lib/angular-toastr/dist/angular-toastr.min.js",
      "wwwroot/lib/angular-toastr/dist/angular-toastr.tpls.min.js",
      "wwwroot/lib/bootstrap/dist/js/bootstrap.min.js",
      "wwwroot/lib/angular-busy/dist/angular-busy.min.js",
      "wwwroot/lib/angular-sanitize/angular-sanitize.min.js",
      "wwwroot/lib/angular-ui-router/release/angular-ui-router.min.js"
    ],
    vendorJsOrder: [
      "**/jquery.min.js",
      "**/angular.js",
      "**/bootstrap.min.js",
      "**/*"
    ],
    vendorCss: [     
      "wwwroot/lib/bootstrap/dist/css/bootstrap.min.css",
      "wwwroot/lib/bootstrap/dist/css/bootstrap-theme.min.css",
      "wwwroot/lib/angular-busy/dist/angular-busy.min.css",
      "wwwroot/lib/angular-toastr/dist/angular-toastr.min.css"
    ]
  }
};

gulp.task('app-js', function () {
    return gulp.src('wwwroot/app/**/*.js')
      .pipe($.sourcemaps.init())
        .pipe($.concat('app-scripts.js'))
        //only uglify if gulp is ran with '--type production'
        .pipe($.uglify())
      .pipe($.sourcemaps.write())
      .pipe(gulp.dest('build'));
});
    
gulp.task("clean", function (cb) {
    
  $.del.sync([
    "./app","./content","./vendor"
  ], {
    force: true
  });
  cb();
});


gulp.task("vendorScripts", function() {
  return gulp
      .src(paths.src.vendorJs)
      .pipe($.plumber())
      .pipe($.angularFilesort())
      .pipe($.order(paths.src.vendorJsOrder))
      .pipe(gulp.dest($.path.join(paths.dist.debug, "vendor")))
      //.pipe($.concat(filenames.min.vendorJs))      
      //.pipe($.streamify($.rev()))
      //.pipe($.rename({ suffix: ".min" }))
      //.pipe(gulp.dest($.path.join(paths.dist.release, "app")))
});

gulp.task("appScripts", function() {
  return gulp
      .src(paths.src.appJs)
      .pipe($.plumber())
      .pipe($.angularFilesort())
      .pipe(gulp.dest($.path.join(paths.dist.debug, "app")))
      //.pipe($.concat(filenames.min.appJs))      
      //.pipe($.bytediff.start())
      //.pipe($.ngAnnotate())
      //.pipe($.streamify($.uglify({ mangle: true })))
      //.pipe($.bytediff.stop())
      //.pipe($.streamify($.rev()))
      //.pipe($.rename({ suffix: ".min" }))
      //.pipe(gulp.dest($.path.join(paths.dist.release, "app")))
});

gulp.task("vendorStyles", function() {
  return gulp
      .src(paths.src.vendorCss)
      .pipe($.plumber())
      .pipe($.flatten())
      .pipe(gulp.dest($.path.join(paths.dist.debug, "content")))
      //.pipe($.concat(filenames.min.vendorCss))      
      //.pipe($.streamify($.rev()))
      //.pipe($.rename({ suffix: ".min" }))
      //.pipe($.streamify($.minifyCss))
      //.pipe($.bytediff.stop())
      //.pipe(gulp.dest($.path.join(paths.dist.release, "content")))
});

gulp.task("appStyles", function () {
  return gulp
      .src(paths.src.appCss)
      .pipe($.plumber())
      .pipe($.flatten())
      .pipe(gulp.dest($.path.join(paths.dist.debug, "content")))
      //.pipe($.concat(filenames.min.appCss))      
      //.pipe($.bytediff.start())
      //.pipe($.streamify($.rev()))
      //.pipe($.rename({ suffix: ".min" }))
      //.pipe($.streamify($.minifyCss))
      //.pipe($.bytediff.stop())
      //.pipe(gulp.dest($.path.join(paths.dist.release, "content")))
});

gulp.task("ngTemplates", function() {
  return gulp
      .src(paths.src.appNgTemplates)
      .pipe($.plumber())
      .pipe(gulp.dest($.path.join(paths.dist.debug, "app")))
      //.pipe($.bytediff.start())
      //.pipe($.minifyHtml({
      //  empty: true,
      //  conditionals: true,
      //  spare: true
      //}))
      //.pipe($.bytediff.stop())
      //.pipe($.angularTemplatecache({
      //  root: "app",
      //  module: "app"
      //}))
      
      //.pipe($.streamify($.rev()))
      //.pipe($.rename({ suffix: ".min" }))
      //.pipe(gulp.dest($.path.join(paths.dist.release, "app")))
});

gulp.task("images", function() {
  return gulp
      .src(paths.src.appImages)
      .pipe($.plumber())
      .pipe(gulp.dest($.path.join(paths.dist.debug, "content")))      
      //.pipe(gulp.dest($.path.join(paths.dist.release, "content")))
});


gulp.task("fonts", function () {
  return gulp
      .src(paths.src.appFonts)
      .pipe($.plumber())
      .pipe($.flatten())
      .pipe(gulp.dest($.path.join(paths.dist.debug, "content")))      
      //.pipe(gulp.dest($.path.join(paths.dist.release, "content/fonts")))
});

gulp.task("app", function(cb) {
    $.runSequence("clean", ["vendorScripts", "appScripts", "vendorStyles", "appStyles",
        "ngTemplates", "images", "fonts"], cb);
});


gulp.task("debug", ["app"], function () {
  var sources = gulp
      .src([
        $.path.join(paths.dist.debug, "vendor/**/*.*"),
        $.path.join(paths.dist.debug, "content/**/*.css"),
        $.path.join(paths.dist.debug, "app/**/*.*")
      ])
      .pipe($.order([
        "vendor/jquery.min.js",
        "vendor/angular.js",
        "vendor/bootstrap.min.js",
        "vendor/**/*.*",
        "**/*"], { base: paths.dist.debug }));

  return gulp
      .src(paths.src.appIndex)
      .pipe($.plumber())
      .pipe($.inject(sources, {
        addRootSlash: false,
        ignorePath: [paths.dist.debug]
      }))
      .pipe($.embedlr())
      .pipe(gulp.dest("."));
});


//gulp.task("watch", ["debug"], function(){
//  gulp.watch(paths.src.vendorJs, ['vendorScripts']);
//  gulp.watch(paths.src.appJs, ['appScripts']);
//  gulp.watch(paths.src.vendorCss, ['vendorStyles']);
//  gulp.watch(paths.src.appCss, ['appStyles']);
//  gulp.watch(paths.src.appNgTemplates, ['ngTemplates']);
//  gulp.watch(paths.src.appImages, ['images']);
//  gulp.watch(paths.src.appFonts, ['fonts']);
//});


////gulp.task("release", ["app"], function () {
////  var sources = gulp
////      .src([
////        $.path.join(paths.dist.release, "content/**/*.*"),
////        $.path.join(paths.dist.release, "app/**/*.*")
////      ])
////      .pipe($.order([
////        "**/vendor*.*",
////        "**/app*.*",
////        "**/templates*.js",
////        "**/*"
////      ], { base: paths.dist.release }));

////  return gulp
////      .src(paths.src.appIndex)
////      .pipe($.plumber())
////      .pipe($.inject(sources, {
////        addRootSlash: false,
////        ignorePath: [paths.dist.release]
////      }))
////      .pipe(gulp.dest(paths.dist.release));
////});
